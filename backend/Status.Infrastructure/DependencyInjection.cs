using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Status.Infrastructure.Persistencia;

namespace Status.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructura(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Default");

        // Configurar DbContext Factory para compatibilidad con Blazor InteractiveServer
        services.AddDbContextFactory<AppDbContext>(options =>
            options.UseNpgsql(connectionString)
                   .UseSnakeCaseNamingConvention());
        
        // También registrar el DbContext normal por si otras capas lo requieren inyectado de forma scoped
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString)
                   .UseSnakeCaseNamingConvention());

        services.AddAuthorization();

        services.AddIdentityApiEndpoints<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }
}
