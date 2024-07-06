namespace Wolny.P.Web.Models;

public class RecorridoModel : BaseModel<RecorridoModel>
{
    public CamionModel? Camion { get; set; }
    public DateTime? FechaFin { get; set; }
    public DateTime? FechaInicio { get; set; }
    public bool Finalizado { get; set; }

    public ICollection<PedidoModel> Pedidos { get; set; }
    public ICollection<PlanRecorridoModel> PlanRecorridos { get; set; }
}
