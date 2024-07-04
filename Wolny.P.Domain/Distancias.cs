namespace Wolny.P.Domain;

public class Distancias : BaseEntity<Distancias>
{
    public Ciudad CiudadOrigen { get; set; }
    public Ciudad CiudadDestino { get; set; }
    public string Observaciones { get; set; } = string.Empty;
}
