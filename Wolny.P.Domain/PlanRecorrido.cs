namespace Wolny.P.Domain;

public class PlanRecorrido : BaseEntity<PlanRecorrido>
{
    public Ciudad Ciudad { get; set; }
    public DateTime? FechaFin { get; set; }
    public bool Finalizado => FechaFin.HasValue;
    public int Prioridad { get; set; }
    // Navigation property
    public int RecorridoId { get; set; }
    public virtual Recorrido? Recorrido { get; set; }
}
