namespace Wolny.P.Web.Models;

public class CiudadModel : BaseModel<CiudadModel>
{
    public string Nombre { get; set; } = string.Empty;
    public GeolocalizacionModel Ubicacion { get; set; }

    //public ICollection<PlanRecorridoModel> PlanRecorridos { get; set; }
}
