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
