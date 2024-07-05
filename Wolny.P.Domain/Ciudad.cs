namespace Wolny.P.Domain;

public class Ciudad : BaseEntity<Ciudad>
{
    public Dictionary<int, int> Distancias { get; set; } = [];
    public string Nombre { get; set; } = string.Empty;
    public Geolocalizacion Ubicacion { get; set; }

    // Navigation props
    public virtual ICollection<PlanRecorrido> PlanRecorridos { get; set; }
}
