namespace Wolny.P.Web.Models;

public class AsignarModel
{
    public List<CamionModel>? Camion { get; set; }
    public int? CamionId { get; set; }
    public RecorridoModel? Recorrido { get; set; }
    public DateTime? FechaInicio { get; set; }
}
