namespace Wolny.P.Web.Models;

public class ResultadoRecorridoModel
{
    public int RecorridoId { get; set; }
    public List<int> PedidoIds { get; set; }
    public string MejorRuta { get; set; }
    public double DistanciaMinima { get; set; }
    public TimeSpan Tiempo { get; set; }
    public long Milisegundos { get; set; }
    public long Ticks { get; set; }
}
