namespace Wolny.P.Domain;

public class Camion : BaseEntity<Camion>
{
    public bool EnViaje { get; set; }
    public string Patente { get; set; }
    public Geolocalizacion Ubicacion { get; set; }
}
