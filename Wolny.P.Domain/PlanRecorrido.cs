namespace Wolny.P.Domain;

public class PlanRecorrido : BaseEntity<PlanRecorrido>
{
    public DateTime? FechaFin { get; set; }
    public bool Finalizado { get; set; }
    public int Prioridad { get; set; }

    // Navigation props
    public int CiudadId { get; set; }
    public virtual Ciudad Ciudad { get; set; }
    public int RecorridoId { get; set; }
    public virtual Recorrido? Recorrido { get; set; }
}
