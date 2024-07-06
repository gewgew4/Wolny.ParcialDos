namespace Wolny.P.Web.Models;

public class PlanRecorridoModel : BaseModel<PlanRecorridoModel>
{
    public DateTime? FechaFin { get; set; }
    public bool Finalizado { get; set; }
    public int Prioridad { get; set; }

    public CiudadModel Ciudad { get; set; }
    //public RecorridoModel? Recorrido { get; set; }
}
