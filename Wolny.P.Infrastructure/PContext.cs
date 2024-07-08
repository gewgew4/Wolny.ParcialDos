using Microsoft.EntityFrameworkCore;
using System.Text.Json;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        // Props
        modelBuilder.Entity<Camion>()
            .Property(x => x.Ubicacion)
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<Geolocalizacion>(v, jsonOptions))
            .HasColumnType("nvarchar(max)");

        modelBuilder.Entity<Recorrido>()
            .HasMany(x => x.PlanRecorridos)
            .WithOne(x => x.Recorrido);

        modelBuilder.Entity<PlanRecorrido>()
            .HasOne(x => x.Recorrido)
            .WithMany(x => x.PlanRecorridos)
            .HasForeignKey(x => x.RecorridoId);

        modelBuilder.Entity<PlanRecorrido>()
            .HasOne(x => x.Ciudad)
            .WithMany(x => x.PlanRecorridos)
            .HasForeignKey(x => x.CiudadId);

        modelBuilder.Entity<Recorrido>()
            .HasMany(x => x.Pedidos)
            .WithOne(x => x.Recorrido);

        modelBuilder.Entity<Pedido>()
            .HasOne(x => x.Recorrido)
            .WithMany(x => x.Pedidos)
            .HasForeignKey(x => x.RecorridoId);

        modelBuilder.Entity<Ciudad>()
            .Property(x => x.Distancias)
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<Dictionary<int, int>>(v, jsonOptions))
            .HasColumnType("nvarchar(max)");

        modelBuilder.Entity<Ciudad>()
            .Property(x => x.Ubicacion)
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<Geolocalizacion>(v, jsonOptions))
            .HasColumnType("nvarchar(max)");

        // Navigation properties settings
        //modelBuilder.Entity<Recorrido>().Navigation(e => e.Pedidos).AutoInclude();
        //modelBuilder.Entity<Recorrido>().Navigation(e => e.PlanRecorridos).AutoInclude();
        modelBuilder.Entity<Pedido>().Navigation(e => e.Ciudad).AutoInclude();
        modelBuilder.Entity<Pedido>().Navigation(e => e.Recorrido).AutoInclude();
        modelBuilder.Entity<PlanRecorrido>().Navigation(e => e.Ciudad).AutoInclude();
        modelBuilder.Entity<PlanRecorrido>().Navigation(e => e.Recorrido).AutoInclude();

        // Seed
        Seed(modelBuilder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        // Seed de ciudades
        var ciudades = new List<Ciudad>
        {
            new Ciudad { Id = 1, Nombre = "CABA", Ubicacion = new Geolocalizacion(-34.60, -58.37) },
            new Ciudad { Id = 2, Nombre = "Córdoba", Ubicacion = new Geolocalizacion(-31.42, -64.18) },
            new Ciudad { Id = 3, Nombre = "Corrientes", Ubicacion = new Geolocalizacion(-27.47, -58.83) },
            new Ciudad { Id = 4, Nombre = "Formosa", Ubicacion = new Geolocalizacion(-26.18, -58.17) },
            new Ciudad { Id = 5, Nombre = "La Plata", Ubicacion = new Geolocalizacion(-34.92, -57.95) },
            new Ciudad { Id = 6, Nombre = "La Rioja", Ubicacion = new Geolocalizacion(-29.41, -66.85) },
            new Ciudad { Id = 7, Nombre = "Mendoza", Ubicacion = new Geolocalizacion(-32.88, -68.84) },
            new Ciudad { Id = 8, Nombre = "Neuquén", Ubicacion = new Geolocalizacion(-38.95, -68.05) }
        };

        // Seed de las distancias según cuadro en consigna
        ciudades[0].Distancias = new Dictionary<int, int>
        {
            { 2, 646 }, { 3, 792 }, { 4, 933 }, { 5, 53 }, { 6, 986 }, { 7, 985 }, { 8, 989 }
        };

        ciudades[1].Distancias = new Dictionary<int, int>
        {
            { 1, 646 }, { 3, 677 }, { 4, 824 }, { 5, 698 }, { 6, 340 }, { 7, 466 }, { 8, 907 }
        };

        ciudades[2].Distancias = new Dictionary<int, int>
        {
            { 1, 792 }, { 2, 677 }, { 4, 157 }, { 5, 830 }, { 6, 814 }, { 7, 1131 }, { 8, 1534 }
        };

        ciudades[3].Distancias = new Dictionary<int, int>
        {
            { 1, 933 }, { 2, 824 }, { 3, 157 }, { 5, 968 }, { 6, 927 }, { 7, 1269 }, { 8, 1690 }
        };

        ciudades[4].Distancias = new Dictionary<int, int>
        {
            { 1, 53 }, { 2, 698 }, { 3, 830 }, { 4, 968 }, { 6, 1038 }, { 7, 1029 }, { 8, 1005 }
        };

        ciudades[5].Distancias = new Dictionary<int, int>
        {
            { 1, 986 }, { 2, 340 }, { 3, 814 }, { 4, 927 }, { 5, 1038 }, { 7, 427 }, { 8, 1063 }
        };

        ciudades[6].Distancias = new Dictionary<int, int>
        {
            { 1, 985 }, { 2, 466 }, { 3, 1131 }, { 4, 1269 }, { 5, 1029 }, { 6, 427 }, { 8, 676 }
        };

        ciudades[7].Distancias = new Dictionary<int, int>
        {
            { 1, 989 }, { 2, 907 }, { 3, 1534 }, { 4, 1690 }, { 5, 1005 }, { 6, 1063 }, { 7, 676 }
        };

        // Agregadas ciudades
        foreach (var ciudad in ciudades)
        {
            modelBuilder.Entity<Ciudad>().HasData(ciudad);
        }

        var geoCaba = new Geolocalizacion(-34.60, -58.37);

        // Seed Camiones
        var camionUsado = new Camion
        {
            Id = 1,
            Disponible = false,
            Patente = "XYZ0",
            Ubicacion = geoCaba
        };
        modelBuilder.Entity<Camion>().HasData(camionUsado);

        for (int i = 2; i < 5; i++)
        {
            var camion = new Camion
            {
                Id = i,
                Disponible = true,
                Patente = $"ABC{i}",
                Ubicacion = geoCaba
            };
            modelBuilder.Entity<Camion>().HasData(camion);
        }

        // Seed Pedidos
        var idPedido = 0;
        var pedidosUsadosFinalizado = new List<Pedido>();
        var pedidosUsadosIncompleto = new List<Pedido>();
        for (int j = 0; j < 40; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                var pedido = new Pedido
                {
                    Id = ++idPedido,
                    Ciudad = ciudades[i],
                    Entregado = false
                };

                // Primer tanda de pedido entregada
                if (idPedido < 9)
                {
                    pedido.Entregado = true;
                    pedidosUsadosFinalizado.Add(pedido);
                }
                // Segunda tanda de pedido entregada
                else if (idPedido < 17)
                {
                    pedido.Entregado = true;
                    pedidosUsadosIncompleto.Add(pedido);
                }
                // Tercera tanda de pedido entregada parcial
                else if (idPedido < 20)
                {
                    pedido.Entregado = true;
                    pedidosUsadosIncompleto.Add(pedido);
                }

                modelBuilder.Entity<Pedido>().HasData(new
                {
                    pedido.Id,
                    CiudadId = pedido.Ciudad.Id,
                    pedido.Entregado
                });
            }
        }
    }
}
