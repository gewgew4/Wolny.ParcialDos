using Wolny.P.Application.Result;
using Wolny.P.Domain;

namespace Wolny.P.Application.Services.Interfaces;

public interface IPedidoService : IGenericService<Pedido>
{
    Task<Result<Pedido>> Add(Pedido entity);
}
