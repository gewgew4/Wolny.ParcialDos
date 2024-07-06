using Wolny.P.Application.Result;
using Wolny.P.Domain;

namespace Wolny.P.Application.Services.Interfaces;

public interface ICamionService : IGenericService<Camion>
{
    Task<Result<List<Camion>>> GetPuntoTres(bool disponible);
}
