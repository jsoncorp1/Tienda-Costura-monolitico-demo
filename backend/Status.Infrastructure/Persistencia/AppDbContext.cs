using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Status.Domain.Entidades;

namespace Status.Infrastructure.Persistencia;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Servicio> Servicios => Set<Servicio>();
    public DbSet<ClienteCorporativo> ClientesCorporativos => Set<ClienteCorporativo>();
    public DbSet<GrupoTarifario> GruposTarifarios => Set<GrupoTarifario>();
    public DbSet<PrecioTarifario> PreciosTarifarios => Set<PrecioTarifario>();
    public DbSet<Solicitud> Solicitudes => Set<Solicitud>();
    public DbSet<SolicitudItem> SolicitudItems => Set<SolicitudItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.HasDefaultSchema("status_app");
        
        builder.Entity<PrecioTarifario>()
            .HasKey(p => new { p.ProductoId, p.GrupoTarifarioId });
    }
}
