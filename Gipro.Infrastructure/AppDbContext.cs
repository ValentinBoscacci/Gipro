// En MiEmpresa.Data/Contexts/AppDbContext.cs
using Core.dominio;
using Microsoft.EntityFrameworkCore;

namespace Gipro.Core.Dominio
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets para cada entidad
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Contratista> Contratistas { get; set; }
        public DbSet<ContratistaContacto> ContratistaContactos { get; set; }
        public DbSet<DocumentoRequerido> DocumentosRequeridos { get; set; }
        public DbSet<DocumentoPresentado> DocumentosPresentados { get; set; }
        public DbSet<AvisoDeTrabajo> AvisosDeTrabajo { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<AspectoEvaluado> AspectosEvaluados { get; set; }
        public DbSet<ResultadoEvaluacion> ResultadosEvaluacion { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones de relaciones y restricciones
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasIndex(e => e.Nombre).IsUnique();
                entity.Property(e => e.ConnectionString).HasColumnType("nvarchar(500)");
            });

            modelBuilder.Entity<Contratista>(entity =>
            {
                entity.HasIndex(c => c.CUIT).IsUnique();
                entity.HasOne(c => c.Empresa)
                      .WithMany(e => e.Contratistas)
                      .HasForeignKey(c => c.EmpresaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para relaciones many-to-many
            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.UsuarioId, ur.RolId });

            // Configuración para herencia (si aplica)
            // modelBuilder.Entity<DocumentoPresentado>()
            //     .HasDiscriminator<string>("TipoDocumento")
            //     .HasValue<DocumentoContratista>("Contratista")
            //     .HasValue<DocumentoEmpleado>("Empleado");

            // Seed inicial de datos
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Administrador" },
                new Rol { Id = 2, Nombre = "Auditor" }
            );
        }
    }
}