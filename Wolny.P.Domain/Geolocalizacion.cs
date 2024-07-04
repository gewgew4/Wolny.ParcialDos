namespace Wolny.P.Domain;

public class Geolocalizacion(double latitud, double longitud)
{
    public double Latitud { get; set; } = latitud;
    public double Longitud { get; set; } = longitud;
}
