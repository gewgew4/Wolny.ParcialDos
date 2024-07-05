using Microsoft.AspNetCore.Mvc;
using Wolny.P.Application.Helpers;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;

namespace Wolny.P.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class CiudadController(ICiudadService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Ciudad>>> GetAll()
    {
        var result = await service.GetAllAsync();

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }
}
