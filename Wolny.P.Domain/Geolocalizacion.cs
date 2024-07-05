using System.ComponentModel.DataAnnotations.Schema;

namespace Wolny.P.Domain;

[NotMapped]
public class Geolocalizacion(double latitud, double longitud)
{
    public double Latitud { get; set; } = latitud;
    public double Longitud { get; set; } = longitud;
}
