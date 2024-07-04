namespace Wolny.P.Domain;

public class Pedido : BaseEntity<Pedido>
{
    public Ciudad Ciudad { get; set; }
    
    
    
    
    
    // Navigation property
    public int RecorridoId { get; set; }
    public virtual Recorrido? Recorrido { get; set; }
}
