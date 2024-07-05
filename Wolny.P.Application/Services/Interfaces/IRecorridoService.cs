using Wolny.P.Application.Models;
using Wolny.P.Application.Result;
using Wolny.P.Domain;

namespace Wolny.P.Application.Services.Interfaces;

public interface IRecorridoService : IGenericService<Recorrido>
{
    Task<Result<Recorrido>> GenerarRecorrido(GenerarRecorrido entity);
}
