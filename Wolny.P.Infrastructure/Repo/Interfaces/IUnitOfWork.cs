namespace Wolny.P.Infrastructure.Repo.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICamionRepo CamionRepo { get; }
    ICiudadRepo CiudadRepo { get; }
    IPedidoRepo PedidoRepo { get; }
    IPlanRecorridoRepo PlanRecorridoRepo { get; }
    IRecorridoRepo RecorridoRepo { get; }
    Task<int> SaveAsync();
}
