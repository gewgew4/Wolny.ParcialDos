using Microsoft.EntityFrameworkCore;
using Wolny.P.Application;
using Wolny.P.Infrastructure;

namespace Wolny.P.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(o=>
            {
                o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();

        builder.Services.InfrastructureConfigureServices(builder.Configuration);
        builder.Services.ApplicationConfigureServices(builder.Configuration);

        builder.Services.AddLogging(loggingBuilder => {
            loggingBuilder.AddConsole()
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            loggingBuilder.AddDebug();
        });


        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.Limits.MaxResponseBufferSize = null;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
