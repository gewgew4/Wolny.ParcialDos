namespace Wolny.P.Web.Models;

public class PedidoModel : BaseModel<PedidoModel>
{
    public CiudadModel Ciudad { get; set; }
    public bool Entregado { get; set; }
    public RecorridoModel Recorrido { get; set; }
}