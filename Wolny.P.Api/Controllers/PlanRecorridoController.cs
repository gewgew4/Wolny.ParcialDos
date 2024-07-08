using Microsoft.AspNetCore.Mvc;
using Wolny.P.Application.Helpers;
using Wolny.P.Application.Models;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;

namespace Wolny.P.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class PlanRecorridoController(IPlanRecorridoService service) : ControllerBase
{
    [HttpPut("ActualizarPlanes")]
    public async Task<ActionResult<List<PlanRecorrido>>> PutActualizarPlanes([FromBody]ActualizarPlanesModel entity)
    {
        var result = await service.ActualizarPlanes(entity);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }
}
