using Microsoft.AspNetCore.Mvc;
using Wolny.P.Application.Helpers;
using Wolny.P.Application.Models;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;

namespace Wolny.P.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class RecorridoController(IRecorridoService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Recorrido>>> GetAll()
    {
        var result = await service.GetAllAsync();

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recorrido>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }

    [HttpGet("puntoDos")]
    public async Task<ActionResult<List<Recorrido>>> GetPuntoDos([FromQuery] PuntoDos request)
    {
        var result = await service.GetPuntoDos(request);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }


    [HttpPut]
    public async Task<ActionResult<Recorrido>> Put(Recorrido entity)
    {
        var result = await service.Update(entity);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }

    [HttpPut("AsignarleCamion")]
    public async Task<ActionResult<Recorrido>> PutAsignarleCamion(AsignarleCamionModel entity)
    {
        var result = await service.AsignarleCamion(entity);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }

    [HttpPost("PuntoUno")]
    public async Task<ActionResult<dynamic>> Post(GenerarRecorrido entity)
    {
        var result = await service.GenerarRecorrido(entity);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }
}