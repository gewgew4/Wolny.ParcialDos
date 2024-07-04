using Wolny.P.Infrastructure.Repo;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly PContext _context;
    public ICamionRepo CamionRepo { get; private set; }
    public ICiudadRepo CiudadRepo { get; private set; }
    public IPedidoRepo PedidoRepo { get; private set; }
    public IPlanRecorridoRepo PlanRecorridoRepo { get; private set; }
    public IRecorridoRepo RecorridoRepo { get; private set; }

    public UnitOfWork(PContext context)
    {
        _context = context;
        CamionRepo = new CamionRepo(_context);
        CiudadRepo = new CiudadRepo(_context);
        PedidoRepo = new PedidoRepo(_context);
        PlanRecorridoRepo = new PlanRecorridoRepo(_context);
        RecorridoRepo = new RecorridoRepo(_context);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
