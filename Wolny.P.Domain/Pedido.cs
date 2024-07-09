namespace Wolny.P.Domain;

public class Pedido : BaseEntity<Pedido>
{
    public bool Entregado { get; set; }

    // Navigation props
    public Ciudad? Ciudad { get; set; }
    public int CiudadId { get; set; }
    public int? RecorridoId { get; set; }
    public virtual Recorrido? Recorrido { get; set; }
}
