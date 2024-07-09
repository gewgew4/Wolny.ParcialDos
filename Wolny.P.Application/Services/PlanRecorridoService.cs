using System.Linq.Expressions;
using Wolny.P.Application.Models;
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

    public async Task<Result<List<PlanRecorrido>>> GetWhereAsync(Expression<Func<PlanRecorrido, bool>> predicate,
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

        existing.FechaFin = entity.FechaFin;
        existing.Finalizado = entity.Finalizado;

        var result = await unitOfWork.PlanRecorridoRepo.Update(existing);
        await unitOfWork.SaveAsync();

        return Result<PlanRecorrido>.Ok(result);
    }

    public async Task<Result<List<PlanRecorrido>>> ActualizarPlanes(ActualizarPlanesModel entity)
    {
        var existingRecorrido = await unitOfWork.RecorridoRepo.GetById(entity.RecorridoId);
        if (existingRecorrido == null)
        {
            return Result<List<PlanRecorrido>>.Fail(ResultType.NotFound);
        }

        var allPlanesRecorrido = (await unitOfWork.PlanRecorridoRepo.GetWhere(x => x.RecorridoId == entity.RecorridoId, includeProperties: ["Ciudad"])).ToList();
        if (allPlanesRecorrido.Count == 0)
        {
            return Result<List<PlanRecorrido>>.Fail(ResultType.NotFound);
        }

        foreach (var item in entity.PlanesRecorrido)
        {
            var existingPlan = allPlanesRecorrido.FirstOrDefault(x => x.Id == item.PlanRecorridoId);
            if (existingPlan == null)
            {
                return Result<List<PlanRecorrido>>.Fail(ResultType.NotFound);
            }
            existingPlan.FechaFin = item.FechaFin;
            existingPlan.Finalizado = true;
            await unitOfWork.PlanRecorridoRepo.Update(existingPlan);
        }

        for (int i = 0; i < allPlanesRecorrido.Where(y => y.Finalizado).Count(); i++)
        {
            var finalizado = allPlanesRecorrido[i].Finalizado;
            if (finalizado == false)
            {
                return Result<List<PlanRecorrido>>.Fail(ResultType.Invalid, ["Seleccionados tramos incorrectamente"]);
            }
        }

        var existingCamion = await unitOfWork.CamionRepo.GetById(entity.CamionId);
        if (existingCamion != null)
        {
            existingCamion.Ubicacion = allPlanesRecorrido.Where(x => x.Finalizado == true).LastOrDefault().Ciudad.Ubicacion;
            await unitOfWork.CamionRepo.Update(existingCamion);
        }

        if (!allPlanesRecorrido.Any(x => x.Finalizado == false))
        {
            existingCamion.Disponible = true;
            existingRecorrido.FechaFin = DateTime.UtcNow;
            existingRecorrido.Finalizado = true;
            await unitOfWork.RecorridoRepo.Update(existingRecorrido);
            await unitOfWork.CamionRepo.Update(existingCamion);
        }

        await unitOfWork.SaveAsync();
        return Result<List<PlanRecorrido>>.Ok(allPlanesRecorrido.ToList());
    }

    public async Task<Result<List<PlanRecorrido>>> EnCamino(int recorridoId)
    {
        var result = await unitOfWork.PlanRecorridoRepo.GetWhere(x => x.RecorridoId == recorridoId);
        result = result.OrderBy(x => x.Prioridad);

        var finalizado = result.FirstOrDefault(x => x.Finalizado == false);
        var previous = result.LastOrDefault(x => x.Finalizado == true);

        return Result<List<PlanRecorrido>>.Ok([previous, finalizado]);
    }
}
