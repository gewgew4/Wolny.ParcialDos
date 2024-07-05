using System.Linq.Expressions;
using Wolny.P.Application.Result;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Application.Services;
public class CiudadService(IUnitOfWork unitOfWork) : ICiudadService
{
    public async Task<Result<Ciudad>> GetByIdAsync(int id)
    {
        var entity = await unitOfWork.CiudadRepo.GetById(id);

        if (entity == null)
        {
            return null;
        }

        return Result<Ciudad>.Ok(entity);
    }

    public async Task<Result<List<Ciudad>>> GetAllAsync()
    {
        var listEntity = await unitOfWork.CiudadRepo.GetAll();

        return Result<List<Ciudad>>.Ok(listEntity);
    }

    public async Task<Result<List<Ciudad>>> GetWheresync(Expression<Func<Ciudad, bool>> predicate,
                                                    Func<IQueryable<Ciudad>,
                                                    IOrderedQueryable<Ciudad>> orderBy = null,
                                                    int? top = null,
                                                    int? skip = null,
                                                    params string[] includeProperties)
    {
        var listEntity = await unitOfWork.CiudadRepo.GetWhere(predicate, orderBy, top, skip, includeProperties);

        return Result<List<Ciudad>>.Ok(listEntity.ToList());
    }

    public async Task<Result<Ciudad>> Update(Ciudad entity)
    {
        var existing = await unitOfWork.CiudadRepo.GetById(entity.Id);
        if (existing == null)
        {
            return Result<Ciudad>.Fail(ResultType.NotFound);
        }

        existing.Distancias = entity.Distancias;
        existing.Nombre = entity.Nombre;
        existing.Ubicacion = entity.Ubicacion;

        await unitOfWork.SaveAsync();

        return Result<Ciudad>.Ok(await unitOfWork.CiudadRepo.Update(existing));
    }
}
