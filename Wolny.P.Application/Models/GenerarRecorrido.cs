using Wolny.P.Application.Helpers;
using Wolny.P.Domain;

namespace Wolny.P.Application.Models;

public class GenerarRecorrido
{
    public List<int> PedidoIds { get; set; }
    public int CamionId { get; set; }
    public AlgoritmoEnum AlgoritmoEnum { get; set; }
}
