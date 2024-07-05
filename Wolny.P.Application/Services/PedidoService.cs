using System.Linq.Expressions;
using Wolny.P.Application.Result;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Application.Services;
public class PedidoService(IUnitOfWork unitOfWork) : IPedidoService
{
    public async Task<Result<Pedido>> GetByIdAsync(int id)
    {
        var entity = await unitOfWork.PedidoRepo.GetById(id);

        if (entity == null)
        {
            return null;
        }

        return Result<Pedido>.Ok(entity);
    }

    public async Task<Result<List<Pedido>>> GetAllAsync()
    {
        var listEntity = await unitOfWork.PedidoRepo.GetAll();

        return Result<List<Pedido>>.Ok(listEntity);
    }

    public async Task<Result<List<Pedido>>> GetWheresync(Expression<Func<Pedido, bool>> predicate,
                                                    Func<IQueryable<Pedido>,
                                                    IOrderedQueryable<Pedido>> orderBy = null,
                                                    int? top = null,
                                                    int? skip = null,
                                                    params string[] includeProperties)
    {
        var listEntity = await unitOfWork.PedidoRepo.GetWhere(predicate, orderBy, top, skip, includeProperties);

        return Result<List<Pedido>>.Ok(listEntity.ToList());
    }

    public async Task<Result<Pedido>> Update(Pedido entity)
    {
        var existing = await unitOfWork.PedidoRepo.GetById(entity.Id);
        if (existing == null)
        {
            return Result<Pedido>.Fail(ResultType.NotFound);
        }

        existing.Ciudad = entity.Ciudad;
        existing.Recorrido = entity.Recorrido;
        existing.RecorridoId = entity.Recorrido.Id;
        
        await unitOfWork.SaveAsync();

        return Result<Pedido>.Ok(await unitOfWork.PedidoRepo.Update(existing));
    }
}
