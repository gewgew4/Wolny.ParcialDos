using Wolny.P.Application.Models;
using Wolny.P.Application.Result;
using Wolny.P.Domain;

namespace Wolny.P.Application.Services.Interfaces;

public interface IPlanRecorridoService : IGenericService<PlanRecorrido>
{
    Task<Result<List<PlanRecorrido>>> ActualizarPlanes(ActualizarPlanesModel entity);
    Task<Result<List<PlanRecorrido>>> EnCamino(int recorridoId);
}
