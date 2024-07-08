using System.Linq.Expressions;
using Wolny.P.Application.Result;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Application.Services;
public class CamionService(IUnitOfWork unitOfWork) : ICamionService
{
    public async Task<Result<Camion>> GetByIdAsync(int id)
    {
        var entity = await unitOfWork.CamionRepo.GetById(id);

        if (entity == null)
        {
            return null;
        }

        return Result<Camion>.Ok(entity);
    }

    public async Task<Result<List<Camion>>> GetAllAsync()
    {
        var listEntity = await unitOfWork.CamionRepo.GetAll();

        return Result<List<Camion>>.Ok(listEntity);
    }

    public async Task<Result<List<Camion>>> GetPuntoTres(bool disponible)
    {
        var listEntity = await unitOfWork.CamionRepo.GetWhere(x=> x.Disponible == disponible);

        return Result<List<Camion>>.Ok(listEntity.ToList());
    }

    public async Task<Result<List<Camion>>> GetWhereAsync(Expression<Func<Camion, bool>> predicate,
                                                    Func<IQueryable<Camion>,
                                                    IOrderedQueryable<Camion>> orderBy = null,
                                                    int? top = null,
                                                    int? skip = null,
                                                    params string[] includeProperties)
    {
        var listEntity = await unitOfWork.CamionRepo.GetWhere(predicate, orderBy, top, skip, includeProperties);

        return Result<List<Camion>>.Ok(listEntity.ToList());
    }

    public async Task<Result<Camion>> Update(Camion entity)
    {
        var existing = await unitOfWork.CamionRepo.GetById(entity.Id);
        if (existing == null)
        {
            return Result<Camion>.Fail(ResultType.NotFound);
        }

        existing.Disponible = entity.Disponible;
        existing.Ubicacion = entity.Ubicacion;

        await unitOfWork.SaveAsync();

        return Result<Camion>.Ok(await unitOfWork.CamionRepo.Update(existing));
    }
}
