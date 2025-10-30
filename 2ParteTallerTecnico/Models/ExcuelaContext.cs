using Microsoft.EntityFrameworkCore;

namespace _2tallertecnico.Models
{
    public class EscuelaContext : DbContext
    {
        public EscuelaContext(DbContextOptions<EscuelaContext> options)
            : base(options)
        {
        }

        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Persona> Personas { get; set; }  // AGREGAR ESTA LÍNEA

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Cargo)
                .WithMany(c => c.Usuarios)
                .HasForeignKey(u => u.IdCargo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.ToTable("Cargo");
                entity.HasKey(e => e.IdCargo);
                entity.Property(e => e.NombreCargo).HasColumnName("NombreCargo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.UsuarioNombre).HasColumnName("Usuario");
                entity.Property(e => e.Apellido).HasColumnName("Apellido");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Password).HasColumnName("Password");
                entity.Property(e => e.Estado).HasColumnName("Estado");
                entity.Property(e => e.Fecha_Registro).HasColumnName("Fecha_Registro");
                entity.Property(e => e.IdCargo).HasColumnName("IdCargo");
            });

            // Persona - Si NO existe tabla en BD, excluir de migraciones
            modelBuilder.Entity<Persona>().ToTable("Persona", t => t.ExcludeFromMigrations());
        }
    }
}