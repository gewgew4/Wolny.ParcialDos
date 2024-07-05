using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolny.P.Application.Services;
using Wolny.P.Application.Services.Interfaces;

namespace Wolny.P.Application;
public static class ApplicationConfigurator
{
    public static IServiceCollection ApplicationConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddScoped<ICamionService, CamionService>();
        services.AddScoped<ICiudadService, CiudadService>();
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<IPlanRecorridoService, PlanRecorridoService>();
        services.AddScoped<IRecorridoService, RecorridoService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}
