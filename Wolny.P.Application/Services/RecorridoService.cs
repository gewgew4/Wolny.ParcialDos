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

            var existingCamion = await unitOfWork.CamionRepo.GetById(entity.CamionId) ?? throw new NotFoundException("Camión inexistente");

            foreach (var pedidoId in entity.PedidoIds)
            {
                var existingPedido = await unitOfWork.PedidoRepo.GetById(pedidoId) ?? throw new NotFoundException("Pedido inexistente");

                if (existingPedido.Entregado == true || existingPedido.Recorrido != null)
                {
                    throw new InvalidException("Pedido ya asignado a un recorrido");
                }

                var ciudadPedido = listaCiudadesExistentes.SingleOrDefault(x => x.Id == existingPedido.Ciudad.Id);
                if (ciudadPedido == null)
                {
                    throw new NotFoundException("Ciudad inexistente");
                }
                else
                {
                    ciudadesPorRecorrer.Add(ciudadPedido);
                    pedidosPorRecorrer.Add(existingPedido);
                }
            }

            ciudadesPorRecorrer = ciudadesPorRecorrer.DistinctBy(x => x.Id).ToList();

            int minDistance;
            int[] bestRoute;
            CalcularFuerzaBruta(ciudadesPorRecorrer, out minDistance, out bestRoute);

            // Output the best route and its distance
            Console.WriteLine("Best route: " + string.Join(" -> ", bestRoute.Select(i => ciudadesPorRecorrer[i].Nombre)) + " -> " + ciudadesPorRecorrer[bestRoute[0]].Nombre);
            Console.WriteLine("Minimum distance: " + minDistance);

            var recorrido = CrearRecorrido(ciudadesPorRecorrer, pedidosPorRecorrer, existingCamion, bestRoute);

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

    private static Recorrido CrearRecorrido(List<Ciudad> ciudadesPorRecorrer, List<Pedido> pedidosPorRecorrer, Camion existingCamion, int[] bestRoute)
    {
        var planRecorridos = new List<PlanRecorrido>();
        for (int i = 0; i < bestRoute.Length; i++)
        {
            planRecorridos.Add(new PlanRecorrido
            {
                CiudadId = ciudadesPorRecorrer[bestRoute[i]].Id,
                Prioridad = i + 1,

            });
        }

        var recorrido = new Recorrido
        {
            Camion = existingCamion,
            FechaInicio = DateTime.UtcNow,
            Pedidos = pedidosPorRecorrer,
            PlanRecorridos = planRecorridos
        };
        return recorrido;
    }

    private static void CalcularFuerzaBruta(List<Ciudad> ciudadesPorRecorrer, out int minDistance, out int[] bestRoute)
    {
        int[,] distanceMatrix = BuildDistanceMatrix(ciudadesPorRecorrer);

        var cities = Enumerable.Range(0, distanceMatrix.GetLength(0)).ToArray();

        // Get all permutations of cities
        var permutations = GetPermutations(cities, cities.Length);

        minDistance = int.MaxValue;
        bestRoute = null;

        // Calculate the distance for each permutation and find the minimum distance
        foreach (var perm in permutations)
        {
            int currentDistance = CalculateRouteDistance(perm, distanceMatrix);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                bestRoute = perm.ToArray();
            }
        }
    }

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

    static IEnumerable<int[]> GetPermutations(int[] list, int length)
    {
        if (length == 1)
            return list.Select(t => new int[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat(new int[] { t2 }).ToArray());
    }
}
