using System.Diagnostics;
using System.Linq.Expressions;
using Wolny.P.Application.Exceptions;
using Wolny.P.Application.Models;
using Wolny.P.Application.Result;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Application.Services;
public class RecorridoService(IUnitOfWork unitOfWork) : IRecorridoService
{
    public async Task<Result<Recorrido>> GetByIdAsync(int id)
    {
        var entity = await unitOfWork.RecorridoRepo.GetById(id);

        if (entity == null)
        {
            return null;
        }

        return Result<Recorrido>.Ok(entity);
    }

    public async Task<Result<List<Recorrido>>> GetAllAsync()
    {
        var listEntity = await unitOfWork.RecorridoRepo.GetAll();

        return Result<List<Recorrido>>.Ok(listEntity);
    }

    public async Task<Result<List<Recorrido>>> GetWheresync(Expression<Func<Recorrido, bool>> predicate,
                                                    Func<IQueryable<Recorrido>,
                                                    IOrderedQueryable<Recorrido>> orderBy = null,
                                                    int? top = null,
                                                    int? skip = null,
                                                    params string[] includeProperties)
    {
        var listEntity = await unitOfWork.RecorridoRepo.GetWhere(predicate, orderBy, top, skip, includeProperties);

        return Result<List<Recorrido>>.Ok(listEntity.ToList());
    }

    public async Task<Result<Recorrido>> Update(Recorrido entity)
    {
        var existing = await unitOfWork.RecorridoRepo.GetById(entity.Id);
        if (existing == null)
        {
            return Result<Recorrido>.Fail(ResultType.NotFound);
        }

        existing.Camion = entity.Camion;
        existing.FechaFin = entity.FechaFin;
        existing.Pedidos = entity.Pedidos;
        existing.PlanRecorridos = entity.PlanRecorridos;

        await unitOfWork.SaveAsync();

        return Result<Recorrido>.Ok(await unitOfWork.RecorridoRepo.Update(existing));
    }

    public async Task<Result<Recorrido>> GenerarRecorrido(GenerarRecorrido entity)
    {
        try
        {
            var ciudadesPorRecorrer = new List<Ciudad>();
            var pedidosPorRecorrer = new List<Pedido>();
            var listaCiudadesExistentes = await unitOfWork.CiudadRepo.GetAll();

            // Validaciones
            var existingCamion = await unitOfWork.CamionRepo.GetById(entity.CamionId) ?? throw new NotFoundException("Camión inexistente");

            foreach (var pedidoId in entity.PedidoIds)
            {
                var existingPedido = await unitOfWork.PedidoRepo.GetById(pedidoId) ?? throw new NotFoundException("Pedido inexistente");

                // Esto chequearía que no haya pedido re/programado ilegalmente, pero por ahora comentado para poder probar los algoritmos
                //if (existingPedido.Entregado == true || existingPedido.Recorrido != null)
                //{
                //    throw new InvalidException("Pedido ya asignado a un recorrido");
                //}

                var ciudadPedido = listaCiudadesExistentes.SingleOrDefault(x => x.Id == existingPedido.Ciudad.Id);
                if (ciudadPedido == null)
                {
                    throw new NotFoundException("Ciudad inexistente");
                }
                else
                {
                    // El pedido está ok y la ciudad existe
                    ciudadesPorRecorrer.Add(ciudadPedido);
                    pedidosPorRecorrer.Add(existingPedido);
                }
            }

            // Elimino duplicados
            ciudadesPorRecorrer = ciudadesPorRecorrer.DistinctBy(x => x.Id).ToList();

            var distanciaMinima = 0;
            int[] mejorRuta = null;

            var sw = new Stopwatch();
            sw.Start();
            switch (entity.AlgoritmoEnum)
            {
                case Helpers.AlgoritmoEnum.FuerzaBruta:
                    CalcularFuerzaBruta(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta);
                    break;
                case Helpers.AlgoritmoEnum.VecinosMasCercanos:
                    CalcularVecinoMasCercano(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta);
                    break;
                case Helpers.AlgoritmoEnum.Genetico:
                    CalcularGenetic(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta);
                    break;
                case Helpers.AlgoritmoEnum.RecocidoSimulado:
                    CalcularRecocidoSimulado(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta);
                    break;
                case Helpers.AlgoritmoEnum.ColoniaHormigas:
                    CalcularColoniaHormigas(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta);
                    break;
                default:
                    throw new NotFoundException("Algoritmo inexistente");
            }
            sw.Stop();

            // Output the best route and its distance
            Console.WriteLine("Best route: " + string.Join(" -> ", mejorRuta.Select(i => ciudadesPorRecorrer[i].Nombre)) + " -> " + ciudadesPorRecorrer[mejorRuta[0]].Nombre);
            Console.WriteLine("Minimum distance: " + distanciaMinima);
            Console.WriteLine($"Elapsed: {sw.Elapsed} - {sw.ElapsedMilliseconds} - {sw.ElapsedTicks}");

            var recorrido = CrearRecorrido(ciudadesPorRecorrer, pedidosPorRecorrer, existingCamion, mejorRuta);

            await unitOfWork.RecorridoRepo.Add(recorrido);
            await unitOfWork.SaveAsync();

            return Result<Recorrido>.Ok(recorrido);
        }
        catch (NotFoundException ex)
        {
            return Result<Recorrido>.Fail(ResultType.NotFound, [ex.Message]);
        }
        catch (InvalidException ex)
        {
            return Result<Recorrido>.Fail(ResultType.Invalid, [ex.Message]);
        }
    }

    private static Recorrido CrearRecorrido(List<Ciudad> ciudadesPorRecorrer, List<Pedido> pedidosPorRecorrer, Camion existingCamion, int[] mejorRuta)
    {
        var planRecorridos = new List<PlanRecorrido>();
        for (int i = 0; i < mejorRuta.Length; i++)
        {
            planRecorridos.Add(new PlanRecorrido
            {
                CiudadId = ciudadesPorRecorrer[mejorRuta[i]].Id,
                Prioridad = i + 1,

            });
        }

        var recorrido = new Recorrido
        {
            Camion = existingCamion,
            Pedidos = pedidosPorRecorrer,
            PlanRecorridos = planRecorridos
        };
        return recorrido;
    }

    #region FuerzaBruta
    private static void CalcularFuerzaBruta(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta)
    {
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);

        var cities = Enumerable.Range(0, distanceMatrix.GetLength(0)).ToArray();

        // Get all permutations of cities
        var permutations = GetPermutations(cities, cities.Length);

        distanciaMinima = int.MaxValue;
        mejorRuta = null;

        // Calculate the distance for each permutation and find the minimum distance
        foreach (var perm in permutations)
        {
            int currentDistance = CalculateRouteDistance(perm, distanceMatrix);
            if (currentDistance < distanciaMinima)
            {
                distanciaMinima = currentDistance;
                mejorRuta = perm.ToArray();
            }
        }
    }

    static IEnumerable<int[]> GetPermutations(int[] list, int length)
    {
        if (length == 1)
            return list.Select(t => new int[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat(new int[] { t2 }).ToArray());
    }
    #endregion

    #region VecinoMasCercano
    private static void CalcularVecinoMasCercano(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta)
    {
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);

        // Find the best route using the Nearest Neighbor heuristic
        mejorRuta = NearestNeighbor(distanceMatrix);
        distanciaMinima = CalculateRouteDistance(mejorRuta, distanceMatrix);
    }

    static int[] NearestNeighbor(int[,] distanceMatrix)
    {
        int n = distanceMatrix.GetLength(0);
        bool[] visited = new bool[n];
        int[] route = new int[n];
        int currentCity = 0;
        visited[0] = true;

        for (int i = 1; i < n; i++)
        {
            int nextCity = -1;
            int minDistance = int.MaxValue;
            for (int j = 0; j < n; j++)
            {
                if (!visited[j] && distanceMatrix[currentCity, j] < minDistance)
                {
                    minDistance = distanceMatrix[currentCity, j];
                    nextCity = j;
                }
            }
            route[i] = nextCity;
            visited[nextCity] = true;
            currentCity = nextCity;
        }

        return route;
    }
    #endregion

    #region Genetic
    private static void CalcularGenetic(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta)
    {
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);

        mejorRuta = GeneticAlgorithm(distanceMatrix, 100, 500, 0.01);
        distanciaMinima = CalculateRouteDistance(mejorRuta, distanceMatrix);
    }

    static int[] GeneticAlgorithm(int[,] distanceMatrix, int populationSize, int generations, double mutationRate)
    {
        int n = distanceMatrix.GetLength(0);
        Random random = new Random();
        List<int[]> population = InitializePopulation(populationSize, n);

        for (int generation = 0; generation < generations; generation++)
        {
            population = population.OrderBy(individual => CalculateRouteDistance(individual, distanceMatrix)).ToList();

            List<int[]> newPopulation = new List<int[]>();

            for (int i = 0; i < populationSize / 2; i++)
            {
                var parent1 = population[random.Next(populationSize / 2)];
                var parent2 = population[random.Next(populationSize / 2)];
                var child1 = Crossover(parent1, parent2, random);
                var child2 = Crossover(parent2, parent1, random);

                if (random.NextDouble() < mutationRate)
                {
                    Mutate(child1, random);
                }

                if (random.NextDouble() < mutationRate)
                {
                    Mutate(child2, random);
                }

                newPopulation.Add(child1);
                newPopulation.Add(child2);
            }

            population = newPopulation;
        }

        return population.OrderBy(individual => CalculateRouteDistance(individual, distanceMatrix)).First();
    }

    static List<int[]> InitializePopulation(int populationSize, int n)
    {
        List<int[]> population = new List<int[]>();
        Random random = new Random();
        for (int i = 0; i < populationSize; i++)
        {
            int[] individual = Enumerable.Range(0, n).OrderBy(x => random.Next()).ToArray();
            population.Add(individual);
        }
        return population;
    }

    static int[] Crossover(int[] parent1, int[] parent2, Random random)
    {
        int n = parent1.Length;
        int start = random.Next(n);
        int end = random.Next(start, n);

        int[] child = new int[n];
        Array.Fill(child, -1);

        for (int i = start; i < end; i++)
        {
            child[i] = parent1[i];
        }

        int currentIndex = end;
        for (int i = 0; i < n; i++)
        {
            int city = parent2[(end + i) % n];
            if (!child.Contains(city))
            {
                child[currentIndex % n] = city;
                currentIndex++;
            }
        }

        return child;
    }

    static void Mutate(int[] individual, Random random)
    {
        int n = individual.Length;
        int index1 = random.Next(n);
        int index2 = (index1 + 1 + random.Next(n - 1)) % n;

        // Swap two cities
        int temp = individual[index1];
        individual[index1] = individual[index2];
        individual[index2] = temp;
    }
    #endregion

    #region RecocidoSimulado
    private static void CalcularRecocidoSimulado(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta)
    {
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);

        // Parameters for Simulated Annealing
        double initialTemperature = 10000;
        double coolingRate = 0.003;

        mejorRuta = SimulatedAnnealing(distanceMatrix, initialTemperature, coolingRate);
        distanciaMinima = CalculateRouteDistance(mejorRuta, distanceMatrix);

        static int[] SimulatedAnnealing(int[,] distanceMatrix, double initialTemperature, double coolingRate)
        {
            int n = distanceMatrix.GetLength(0);
            var random = new Random();

            int[] currentSolution = Enumerable.Range(0, n).ToArray();
            int[] bestSolution = (int[])currentSolution.Clone();

            double currentTemperature = initialTemperature;

            while (currentTemperature > 1)
            {
                int[] newSolution = (int[])currentSolution.Clone();

                int swapIndex1 = random.Next(n);
                int swapIndex2 = (swapIndex1 + random.Next(1, n)) % n;

                // Swap two cities
                int temp = newSolution[swapIndex1];
                newSolution[swapIndex1] = newSolution[swapIndex2];
                newSolution[swapIndex2] = temp;

                int currentEnergy = CalculateRouteDistance(currentSolution, distanceMatrix);
                int newEnergy = CalculateRouteDistance(newSolution, distanceMatrix);

                if (AcceptanceProbability(currentEnergy, newEnergy, currentTemperature) > random.NextDouble())
                {
                    currentSolution = newSolution;
                }

                if (CalculateRouteDistance(currentSolution, distanceMatrix) < CalculateRouteDistance(bestSolution, distanceMatrix))
                {
                    bestSolution = (int[])currentSolution.Clone();
                }

                currentTemperature *= 1 - coolingRate;
            }

            return bestSolution;
        }

        static double AcceptanceProbability(int currentEnergy, int newEnergy, double temperature)
        {
            if (newEnergy < currentEnergy)
            {
                return 1.0;
            }
            return Math.Exp((currentEnergy - newEnergy) / temperature);
        }
    }
    #endregion

    #region ColoniaHormigas
    private static void CalcularColoniaHormigas(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta)
    {
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);

        // Parameters for Ant Colony Optimization
        int numberOfAnts = 20;
        int numberOfIterations = 100;
        double alpha = 1.0; // Pheromone importance
        double beta = 2.0;  // Distance importance
        double evaporationRate = 0.5;
        double initialPheromoneValue = 1.0;

        mejorRuta = AntColonyOptimization(distanceMatrix, numberOfAnts, numberOfIterations, alpha, beta, evaporationRate, initialPheromoneValue);
        distanciaMinima = CalculateRouteDistance(mejorRuta, distanceMatrix);

        static int[] AntColonyOptimization(int[,] distanceMatrix, int numberOfAnts, int numberOfIterations, double alpha, double beta, double evaporationRate, double initialPheromoneValue)
        {
            int n = distanceMatrix.GetLength(0);
            double[,] pheromoneLevels = new double[n, n];
            Random random = new Random();

            // Initialize pheromone levels
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    pheromoneLevels[i, j] = initialPheromoneValue;
                }
            }

            int[] bestSolution = null;
            int bestSolutionLength = int.MaxValue;

            for (int iteration = 0; iteration < numberOfIterations; iteration++)
            {
                int[][] solutions = new int[numberOfAnts][];
                int[] solutionLengths = new int[numberOfAnts];

                for (int k = 0; k < numberOfAnts; k++)
                {
                    solutions[k] = GenerateSolution(n, distanceMatrix, pheromoneLevels, alpha, beta, random);
                    solutionLengths[k] = CalculateRouteDistance(solutions[k], distanceMatrix);

                    if (solutionLengths[k] < bestSolutionLength)
                    {
                        bestSolution = solutions[k];
                        bestSolutionLength = solutionLengths[k];
                    }
                }

                // Evaporate pheromones
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        pheromoneLevels[i, j] *= (1 - evaporationRate);
                    }
                }

                // Deposit new pheromones
                for (int k = 0; k < numberOfAnts; k++)
                {
                    for (int i = 0; i < n - 1; i++)
                    {
                        pheromoneLevels[solutions[k][i], solutions[k][i + 1]] += 1.0 / solutionLengths[k];
                    }
                    pheromoneLevels[solutions[k][n - 1], solutions[k][0]] += 1.0 / solutionLengths[k];
                }
            }

            return bestSolution;
        }

        static int[] GenerateSolution(int n, int[,] distanceMatrix, double[,] pheromoneLevels, double alpha, double beta, Random random)
        {
            List<int> unvisitedCities = Enumerable.Range(0, n).ToList();
            int[] solution = new int[n];
            int currentCity = random.Next(n);
            solution[0] = currentCity;
            unvisitedCities.Remove(currentCity);

            for (int i = 1; i < n; i++)
            {
                int nextCity = SelectNextCity(currentCity, unvisitedCities, distanceMatrix, pheromoneLevels, alpha, beta, random);
                solution[i] = nextCity;
                unvisitedCities.Remove(nextCity);
                currentCity = nextCity;
            }

            return solution;
        }

        static int SelectNextCity(int currentCity, List<int> unvisitedCities, int[,] distanceMatrix, double[,] pheromoneLevels, double alpha, double beta, Random random)
        {
            double[] probabilities = new double[unvisitedCities.Count];
            double sum = 0.0;

            for (int i = 0; i < unvisitedCities.Count; i++)
            {
                int nextCity = unvisitedCities[i];
                probabilities[i] = Math.Pow(pheromoneLevels[currentCity, nextCity], alpha) * Math.Pow(1.0 / distanceMatrix[currentCity, nextCity], beta);
                sum += probabilities[i];
            }

            double randomValue = random.NextDouble() * sum;
            double cumulative = 0.0;

            for (int i = 0; i < probabilities.Length; i++)
            {
                cumulative += probabilities[i];
                if (cumulative >= randomValue)
                {
                    return unvisitedCities[i];
                }
            }

            return unvisitedCities[0];
        }
    }
    #endregion

    #region General
    static int[,] BuildDistanceMatrix(List<Ciudad> ciudades)
    {
        int n = ciudades.Count;
        int[,] distanceMatrix = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                {
                    distanceMatrix[i, j] = 0;
                }
                else
                {
                    distanceMatrix[i, j] = ciudades[i].Distancias[ciudades[j].Id];
                }
            }
        }

        return distanceMatrix;
    }

    static int CalculateRouteDistance(int[] route, int[,] distanceMatrix)
    {
        int totalDistance = 0;
        for (int i = 0; i < route.Length - 1; i++)
        {
            totalDistance += distanceMatrix[route[i], route[i + 1]];
        }

        // Add the distance back to the starting city
        totalDistance += distanceMatrix[route[route.Length - 1], route[0]];

        return totalDistance;
    }
    #endregion
}
