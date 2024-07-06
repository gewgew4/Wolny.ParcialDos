using Microsoft.AspNetCore.Mvc;
using Wolny.P.Application.Helpers;
using Wolny.P.Application.Services.Interfaces;
using Wolny.P.Domain;

namespace Wolny.P.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class PedidoController(IPedidoService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Pedido>>> GetAll()
    {
        var result = await service.GetAllAsync();

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }

    [HttpGet("SinRecorrido")]
    public async Task<ActionResult<List<Pedido>>> GetSinRecorrido()
    {
        var result = await service.GetWhereAsync(x=> x.RecorridoId == null || x.RecorridoId == default);

        if (result.Success)
        {
            return Ok(result);
        }

        return WebApiResponse.GetErrorResponse(result);
    }
}
