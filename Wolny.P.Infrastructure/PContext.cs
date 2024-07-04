using Microsoft.EntityFrameworkCore;
using Wolny.P.Domain;

namespace Wolny.P.Infrastructure;

public class PContext : DbContext
{
    public PContext(DbContextOptions<PContext> options) : base(options) { }
    public DbSet<Camion> Camiones { get; set; }
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PlanRecorrido> PlanRecorridos { get; set; }
    public DbSet<Recorrido> Recorridos { get; set; }

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    public void Migrate()
    {
        base.Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Props
        modelBuilder.Entity<Recorrido>()
            .HasMany(x => x.PlanRecorridos)
            .WithOne(x => x.Recorrido);

        modelBuilder.Entity<PlanRecorrido>()
            .HasOne(x => x.Recorrido)
            .WithMany(x => x.PlanRecorridos)
            .HasForeignKey(x => x.RecorridoId);

        modelBuilder.Entity<Recorrido>()
            .HasMany(x => x.Pedidos)
            .WithOne(x => x.Recorrido);

        modelBuilder.Entity<Pedido>()
            .HasOne(x => x.Recorrido)
            .WithMany(x => x.Pedidos)
            .HasForeignKey(x => x.RecorridoId);

        // Navigation properties settings
        modelBuilder.Entity<Recorrido>().Navigation(e => e.Pedidos).AutoInclude();
        modelBuilder.Entity<Recorrido>().Navigation(e => e.PlanRecorridos).AutoInclude();
        modelBuilder.Entity<Pedido>().Navigation(e => e.Ciudad).AutoInclude();
        modelBuilder.Entity<Pedido>().Navigation(e => e.Recorrido).AutoInclude();
        modelBuilder.Entity<PlanRecorrido>().Navigation(e => e.Ciudad).AutoInclude();
        modelBuilder.Entity<PlanRecorrido>().Navigation(e => e.Recorrido).AutoInclude();
    }
}
