namespace Wolny.P.Web.Models;

public class CamionModel : BaseModel<CamionModel>
{
    public bool Disponible { get; set; }
    public string Patente { get; set; }
    public GeolocalizacionModel Ubicacion { get; set; }
}
