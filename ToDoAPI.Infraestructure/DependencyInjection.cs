// Importamos EF Core para configurar el DbContext
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// Importamos las interfaces de Application
using ToDoAPI.Application.Interfaces;
// Importamos el Service de Application
using ToDoAPI.Application.Services;
// Importamos las interfaces de Domain
using ToDoAPI.Domain.Interfaces;
// Importamos el DbContext y UnitOfWork de Infrastructure
using ToDoAPI.Infrastructure.Data;

namespace ToDoAPI.Infrastructure;

// Clase estática con un método de extensión sobre IServiceCollection
// Esto nos permite registrar todo Infrastructure en Program.cs con una sola línea
public static class DependencyInjection
{
    // "this IServiceCollection services" significa que este método
    // se puede llamar directamente sobre services en Program.cs así:
    // builder.Services.AddInfrastructure(builder.Configuration)
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Registra el DbContext con SQL Server
        // La connection string viene de appsettings.json
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Cuando alguien pida IUnitOfWork → dale UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Cuando alguien pida ITodoService → dale TodoService
        services.AddScoped<ITodoService, TodoService>();

        // Retorna services para poder encadenar más registros si fuera necesario
        return services;
    }
}