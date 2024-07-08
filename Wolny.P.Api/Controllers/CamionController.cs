using Microsoft.AspNetCore.Mvc;
using Wolny.P.Application.Helpers;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;

namespace Wolny.P.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class CamionController(ICamionService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Camion>>> GetAll()
    {
        var result = await service.GetAllAsync();

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Camion>>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }

    [HttpGet("PuntoTres")]
    public async Task<ActionResult<List<Camion>>> GetPuntoTres([FromQuery] bool disponible)
    {
        var result = await service.GetPuntoTres(disponible);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }

    [HttpGet("PuntoCuatro")]
    public async Task<ActionResult<List<Camion>>> GetPuntoCuatro()
    {
        // Uso la prop Disponible de Camion para saber si está en viaje o no
        var result = await service.GetPuntoTres(false);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }

    [HttpPut]
    public async Task<ActionResult<Camion>> Put(Camion entity)
    {
        var result = await service.Update(entity);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }
}
