namespace Wolny.P.Web.Models;

public class ActualizarPlanesModel
{
    public List<PlanRecorridoActulizadoModel>? PlanesRecorrido { get; set; }
    public int? RecorridoId { get; set; }
    public int? CamionId { get; set; }
    public string CamionPatente { get; set; }
}

public class PlanRecorridoActulizadoModel
{
    public int PlanRecorridoId { get; set; }
    public bool IsSelected { get; set; }
    public DateTime? FechaFin { get; set; }
}
