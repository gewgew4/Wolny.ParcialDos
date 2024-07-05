using System.Linq.Expressions;
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
}
