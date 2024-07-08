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
        var entity = await unitOfWork.RecorridoRepo.GetById(id, includeProperties: ["Camion", "Pedidos", "PlanRecorridos"]);

        if (entity == null)
        {
            return null;
        }

        return Result<Recorrido>.Ok(entity);
    }

    public async Task<Result<List<Recorrido>>> GetAllAsync()
    {
        var listEntity = await unitOfWork.RecorridoRepo.GetAll(includeProperties: "Camion");

        return Result<List<Recorrido>>.Ok(listEntity);
    }

    public async Task<Result<List<Recorrido>>> GetPuntoDos(PuntoDos request)
    {
        var listEntity = await unitOfWork.RecorridoRepo.GetWhere(
            x => x.Camion.Patente == request.Patente && x.Finalizado == request.Finalizado,
            request.Ascending ? o => o.OrderBy(y => y.FechaInicio) : o => o.OrderByDescending(y => y.FechaInicio),
            request.Top,
            includeProperties: ["Camion"]);

        return Result<List<Recorrido>>.Ok(listEntity.ToList());
    }

    public async Task<Result<List<Recorrido>>> GetWhereAsync(Expression<Func<Recorrido, bool>> predicate,
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

    public async Task<Result<dynamic>> GenerarRecorrido(GenerarRecorrido entity)
    {
        try
        {
            var ciudadesPorRecorrer = new List<Ciudad>();
            var pedidosPorRecorrer = new List<Pedido>();
            Camion? existingCamion = null;
            var listaCiudadesExistentes = await unitOfWork.CiudadRepo.GetAll();

            // Validaciones
            if (entity.CamionId.HasValue)
            {
                existingCamion = await unitOfWork.CamionRepo.GetById(entity.CamionId.Value) ?? throw new NotFoundException("Camión inexistente");
            }

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

            var origenCiudad = listaCiudadesExistentes.FirstOrDefault(x => x.Id == entity.OrigenCiudadId) ?? throw new NotFoundException("Ciudad de origen inexistente");

            // Agrego la ciudad de origen
            ciudadesPorRecorrer.Add(origenCiudad);
            // Elimino duplicados
            ciudadesPorRecorrer = ciudadesPorRecorrer.DistinctBy(x => x.Id).ToList();

            // Bench tiempo
            var sw = new Stopwatch();
            sw.Start();
            ElegirAlgoritmo(entity, ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta);
            sw.Stop();

            Console.WriteLine("Mejor ruta: " + string.Join(" -> ", mejorRuta.Select(i => ciudadesPorRecorrer[i].Nombre)) + " -> " + ciudadesPorRecorrer[mejorRuta[0]].Nombre);
            Console.WriteLine("Distancia mínima: " + distanciaMinima);
            Console.WriteLine($"Tiempo - milisegundos - ticks: {sw.Elapsed} - {sw.ElapsedMilliseconds} - {sw.ElapsedTicks}");

            var recorrido = CrearRecorrido(ciudadesPorRecorrer, pedidosPorRecorrer, existingCamion, mejorRuta);

            await unitOfWork.RecorridoRepo.Add(recorrido);
            await unitOfWork.SaveAsync();

            var newRecorrido = new
            {
                RecorridoId = recorrido.Id,
                entity.PedidoIds,
                MejorRuta = string.Join(" -> ", mejorRuta.Select(i => ciudadesPorRecorrer[i].Nombre)) + " -> " + ciudadesPorRecorrer[mejorRuta[0]].Nombre,
                DistanciaMinima = distanciaMinima,
                Tiempo = sw.Elapsed,
                Milisegundos = sw.ElapsedMilliseconds,
                Ticks = sw.ElapsedTicks
            };

            return Result<dynamic>.Ok(newRecorrido);
        }
        catch (NotFoundException ex)
        {
            return Result<dynamic>.Fail(ResultType.NotFound, [ex.Message]);
        }
        catch (InvalidException ex)
        {
            return Result<dynamic>.Fail(ResultType.Invalid, [ex.Message]);
        }
    }

    private static void ElegirAlgoritmo(GenerarRecorrido entity, List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta)
    {
        switch (entity.AlgoritmoEnum)
        {
            case Helpers.AlgoritmoEnum.FuerzaBruta:
                CalcularFuerzaBruta(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta, entity.OrigenCiudadId);
                break;
            case Helpers.AlgoritmoEnum.VecinosMasCercanos:
                CalcularVecinoMasCercano(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta, entity.OrigenCiudadId);
                break;
            case Helpers.AlgoritmoEnum.Voraz:
                CalcularVoraz(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta, entity.OrigenCiudadId);
                break;
            case Helpers.AlgoritmoEnum.RecocidoSimulado:
                CalcularRecocidoSimulado(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta, entity.OrigenCiudadId);
                break;
            case Helpers.AlgoritmoEnum.ColoniaHormigas:
                CalcularColoniaHormigas(ciudadesPorRecorrer, out distanciaMinima, out mejorRuta, entity.OrigenCiudadId);
                break;
            default:
                throw new NotFoundException("Algoritmo inexistente");
        }
    }

    private static Recorrido CrearRecorrido(List<Ciudad> ciudadesPorRecorrer, List<Pedido> pedidosPorRecorrer, Camion? existingCamion, int[] mejorRuta)
    {
        var planRecorridos = new List<PlanRecorrido>();
        for (int i = 0; i < mejorRuta.Length; i++)
        {
            planRecorridos.Add(new PlanRecorrido
            {
                CiudadId = ciudadesPorRecorrer[mejorRuta[i]].Id,
                Prioridad = i + 1
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
    private static void CalcularFuerzaBruta(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta, int origenCiudadId)
    {
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);
        int originIndex = ciudadesPorRecorrer.FindIndex(c => c.Id == origenCiudadId);

        var cities = Enumerable.Range(0, distanceMatrix.GetLength(0)).Where(i => i != originIndex).ToArray();

        // Get all permutations of cities
        var permutations = GetPermutations(cities, cities.Length);

        distanciaMinima = int.MaxValue;
        mejorRuta = null;

        // Calculate the distance for each permutation and find the minimum distance
        foreach (var perm in permutations)
        {
            var route = new int[perm.Length + 1];
            route[0] = originIndex;
            Array.Copy(perm, 0, route, 1, perm.Length);

            int currentDistance = CalculateRouteDistance(route, distanceMatrix);
            if (currentDistance < distanciaMinima)
            {
                distanciaMinima = currentDistance;
                mejorRuta = route;
            }
        }
    }

    static IEnumerable<int[]> GetPermutations(int[] list, int length)
    {
        if (length == 1)
            return list.Select(t => new int[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat([t2]).ToArray());
    }
    #endregion

    #region VecinoMasCercano
    private static void CalcularVecinoMasCercano(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta, int origenCiudadId)
    {
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);
        int originIndex = ciudadesPorRecorrer.FindIndex(c => c.Id == origenCiudadId);

        // Find the best route using the Nearest Neighbor heuristic
        mejorRuta = NearestNeighbor(distanceMatrix, originIndex);
        distanciaMinima = CalculateRouteDistance(mejorRuta, distanceMatrix);
    }

    static int[] NearestNeighbor(int[,] distanceMatrix, int originIndex)
    {
        int n = distanceMatrix.GetLength(0);
        bool[] visited = new bool[n];
        int[] route = new int[n];
        int currentCity = originIndex;
        visited[originIndex] = true;

        for (int i = 1; i < n; i++)
        {
            int nearestCity = -1;
            int nearestDistance = int.MaxValue;

            for (int j = 0; j < n; j++)
            {
                if (!visited[j] && distanceMatrix[currentCity, j] < nearestDistance)
                {
                    nearestCity = j;
                    nearestDistance = distanceMatrix[currentCity, j];
                }
            }

            route[i] = nearestCity;
            visited[nearestCity] = true;
            currentCity = nearestCity;
        }

        return route;
    }
    #endregion

    #region Voraz
    private static void CalcularVoraz(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta, int origenCiudadId)
    {
        int numberOfCities = ciudadesPorRecorrer.Count;
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);
        int originIndex = ciudadesPorRecorrer.FindIndex(c => c.Id == origenCiudadId);

        bool[] visited = new bool[numberOfCities];
        List<int> route = new List<int>();

        route.Add(originIndex);
        visited[originIndex] = true;
        int currentCity = originIndex;
        distanciaMinima = 0;

        for (int i = 1; i < numberOfCities; i++)
        {
            int nearestCity = -1;
            int shortestDistance = int.MaxValue;

            for (int j = 0; j < numberOfCities; j++)
            {
                if (!visited[j] && distanceMatrix[currentCity, j] < shortestDistance)
                {
                    nearestCity = j;
                    shortestDistance = distanceMatrix[currentCity, j];
                }
            }

            if (nearestCity != -1)
            {
                route.Add(nearestCity);
                visited[nearestCity] = true;
                distanciaMinima += shortestDistance;
                currentCity = nearestCity;
            }
        }

        // Return to the origin city
        route.Add(originIndex);
        distanciaMinima += distanceMatrix[currentCity, originIndex];
        mejorRuta = route.ToArray();
    }
    #endregion

    #region RecocidoSimulado
    private static void CalcularRecocidoSimulado(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta, int origenCiudadId)
    {
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);
        int originIndex = ciudadesPorRecorrer.FindIndex(c => c.Id == origenCiudadId);

        int n = ciudadesPorRecorrer.Count;
        int[] currentSolution = new int[n];
        int[] bestSolution = new int[n];
        for (int i = 0; i < n; i++)
        {
            currentSolution[i] = i;
            bestSolution[i] = i;
        }

        // Ensure the origin city is at the start of the route
        Swap(ref currentSolution[0], ref currentSolution[originIndex]);
        Swap(ref bestSolution[0], ref bestSolution[originIndex]);

        int currentDistance = CalculateRouteDistance(currentSolution, distanceMatrix);
        int bestDistance = currentDistance;

        double temperature = 10000;
        double coolingRate = 0.9995;
        int iterations = 100000;

        Random rng = new Random();

        for (int i = 0; i < iterations; i++)
        {
            int[] newSolution = (int[])currentSolution.Clone();

            int swapIndex1 = rng.Next(1, n);
            int swapIndex2 = rng.Next(1, n);

            Swap(ref newSolution[swapIndex1], ref newSolution[swapIndex2]);

            int newDistance = CalculateRouteDistance(newSolution, distanceMatrix);

            if (AcceptanceProbability(currentDistance, newDistance, temperature) > rng.NextDouble())
            {
                currentSolution = newSolution;
                currentDistance = newDistance;
            }

            if (currentDistance < bestDistance)
            {
                bestSolution = (int[])currentSolution.Clone();
                bestDistance = currentDistance;
            }

            temperature *= coolingRate;
        }

        distanciaMinima = bestDistance;
        mejorRuta = bestSolution;
    }

    private static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    private static double AcceptanceProbability(int currentDistance, int newDistance, double temperature)
    {
        if (newDistance < currentDistance)
        {
            return 1.0;
        }
        return Math.Exp((currentDistance - newDistance) / temperature);
    }
    #endregion

    #region ColoniaHormigas
    private static void CalcularColoniaHormigas(List<Ciudad> ciudadesPorRecorrer, out int distanciaMinima, out int[] mejorRuta, int originCityId)
    {
        int n = ciudadesPorRecorrer.Count;
        int originIndex = ciudadesPorRecorrer.FindIndex(c => c.Id == originCityId);
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);

        double alpha = 1.0;  // Pheromone importance
        double beta = 2.0;   // Distance importance
        double evaporation = 0.5;
        double Q = 100;  // Total amount of pheromone
        int numAnts = 10;
        int maxIterations = 100;

        double[,] pheromones = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                pheromones[i, j] = 1.0;
            }
        }

        int[] bestRoute = null;
        int bestDistance = int.MaxValue;
        Random rng = new Random();

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            List<int[]> routes = new List<int[]>();
            List<int> distances = new List<int>();

            for (int ant = 0; ant < numAnts; ant++)
            {
                int[] route = new int[n];
                bool[] visited = new bool[n];
                route[0] = originIndex;
                visited[originIndex] = true;

                for (int i = 1; i < n; i++)
                {
                    int currentCity = route[i - 1];
                    int nextCity = SelectNextCity(currentCity, visited, pheromones, distanceMatrix, alpha, beta, rng);
                    route[i] = nextCity;
                    visited[nextCity] = true;
                }

                int routeDistance = CalculateRouteDistance(route, distanceMatrix);
                routes.Add(route);
                distances.Add(routeDistance);

                if (routeDistance < bestDistance)
                {
                    bestDistance = routeDistance;
                    bestRoute = (int[])route.Clone();
                }
            }

            // Update pheromones
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    pheromones[i, j] *= (1 - evaporation);
                }
            }

            for (int ant = 0; ant < numAnts; ant++)
            {
                int[] route = routes[ant];
                int routeDistance = distances[ant];

                for (int i = 0; i < n - 1; i++)
                {
                    int city1 = route[i];
                    int city2 = route[i + 1];
                    pheromones[city1, city2] += Q / routeDistance;
                    pheromones[city2, city1] += Q / routeDistance;
                }
                int lastCity = route[n - 1];
                int firstCity = route[0];
                pheromones[lastCity, firstCity] += Q / routeDistance;
                pheromones[firstCity, lastCity] += Q / routeDistance;
            }
        }

        distanciaMinima = bestDistance;
        mejorRuta = bestRoute;
    }

    private static int SelectNextCity(int currentCity, bool[] visited, double[,] pheromones, int[,] distanceMatrix, double alpha, double beta, Random rng)
    {
        int n = visited.Length;
        double[] probabilities = new double[n];
        double sum = 0.0;

        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                probabilities[i] = Math.Pow(pheromones[currentCity, i], alpha) * Math.Pow(1.0 / distanceMatrix[currentCity, i], beta);
                sum += probabilities[i];
            }
        }

        double randomValue = rng.NextDouble() * sum;
        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                randomValue -= probabilities[i];
                if (randomValue <= 0)
                {
                    return i;
                }
            }
        }

        // Fallback in case of rounding errors
        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                return i;
            }
        }

        return -1;  // Should never reach here
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
