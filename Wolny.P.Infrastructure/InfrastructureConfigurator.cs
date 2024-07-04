using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolny.P.Infrastructure.Repo;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Infrastructure;

public static class InfrastructureConfigurator
{
    public static IServiceCollection InfrastructureConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<PContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<ICamionRepo, CamionRepo>();
        services.AddScoped<ICiudadRepo, CiudadRepo>();
        services.AddScoped<IPedidoRepo, PedidoRepo>();
        services.AddScoped<IPlanRecorridoRepo, PlanRecorridoRepo>();
        services.AddScoped<IRecorridoRepo, RecorridoRepo>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
