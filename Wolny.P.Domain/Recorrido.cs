namespace Wolny.P.Domain;

public class Recorrido : BaseEntity<Recorrido>
{
    public Camion? Camion { get; set; }
    public DateTime? FechaFin { get; set; }
    public DateTime FechaInicio { get; set; }
    public bool Finalizado { get; set; }

    // Navigation props
    public virtual ICollection<Pedido> Pedidos { get; set; }
    public virtual ICollection<PlanRecorrido> PlanRecorridos { get; set; }
}
