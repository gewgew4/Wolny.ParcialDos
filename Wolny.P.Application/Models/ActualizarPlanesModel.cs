namespace Wolny.P.Application.Models;

public class ActualizarPlanesModel
{
    public List<PlanRecorridoModel> PlanesRecorrido { get; set; } 
    public int RecorridoId { get; set; }
    public int CamionId { get; set; }
}

public class PlanRecorridoModel
{
    public int PlanRecorridoId { get; set; }
    public DateTime FechaFin { get; set; }
}
