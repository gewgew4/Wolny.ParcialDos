using System.Linq.Expressions;
using Wolny.P.Application.Result;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Application.Services;
public class PlanRecorridoService(IUnitOfWork unitOfWork) : IPlanRecorridoService
{
    public async Task<Result<PlanRecorrido>> GetByIdAsync(int id)
    {
        var entity = await unitOfWork.PlanRecorridoRepo.GetById(id);

        if (entity == null)
        {
            return null;
        }

        return Result<PlanRecorrido>.Ok(entity);
    }

    public async Task<Result<List<PlanRecorrido>>> GetAllAsync()
    {
        var listEntity = await unitOfWork.PlanRecorridoRepo.GetAll();

        return Result<List<PlanRecorrido>>.Ok(listEntity);
    }

    public async Task<Result<List<PlanRecorrido>>> GetWheresync(Expression<Func<PlanRecorrido, bool>> predicate,
                                                    Func<IQueryable<PlanRecorrido>,
                                                    IOrderedQueryable<PlanRecorrido>> orderBy = null,
                                                    int? top = null,
                                                    int? skip = null,
                                                    params string[] includeProperties)
    {
        var listEntity = await unitOfWork.PlanRecorridoRepo.GetWhere(predicate, orderBy, top, skip, includeProperties);

        return Result<List<PlanRecorrido>>.Ok(listEntity.ToList());
    }

    public async Task<Result<PlanRecorrido>> Update(PlanRecorrido entity)
    {
        var existing = await unitOfWork.PlanRecorridoRepo.GetById(entity.Id);
        if (existing == null)
        {
            return Result<PlanRecorrido>.Fail(ResultType.NotFound);
        }

        existing.Ciudad = entity.Ciudad;
        existing.FechaFin = entity.FechaFin;
        existing.Prioridad = entity.Prioridad;
        existing.Recorrido = entity.Recorrido;
        existing.RecorridoId = entity.Recorrido.Id;

        await unitOfWork.SaveAsync();

        return Result<PlanRecorrido>.Ok(await unitOfWork.PlanRecorridoRepo.Update(existing));
    }
}
