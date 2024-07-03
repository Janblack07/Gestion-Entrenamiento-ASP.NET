using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestionEntrenamientoDeportivo.Models
{
    public class DBgestion : IdentityDbContext<Usuario>
    {
        public DBgestion(DbContextOptions<DBgestion> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rutina> Rutinas { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<RegistroProgreso> RegistrosProgreso { get; set; }
        public DbSet<CategoriaEjercicio> EjercicioCategorias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Usuario - Rutina
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Rutinas)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.UsuarioId);

            // Relación Usuario - RegistroProgreso
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.RegistrosProgreso)
                .WithOne(rp => rp.Usuario)
                .HasForeignKey(rp => rp.UsuarioId);

            // Relación Rutina - Ejercicio
            modelBuilder.Entity<Rutina>()
                .HasMany(r => r.Ejercicios)
                .WithOne(rp => rp.Rutina)
                .HasForeignKey(rp => rp.RutinaId);

            // Relación muchos a muchos Ejercicio - Categoria
            modelBuilder.Entity<CategoriaEjercicio>()
                .HasKey(ec => new { ec.EjercicioId, ec.CategoriaId });

            modelBuilder.Entity<CategoriaEjercicio>()
                .HasOne(ec => ec.Ejercicio)
                .WithMany(e => e.EjercicioCategorias)
                .HasForeignKey(ec => ec.EjercicioId);

            modelBuilder.Entity<CategoriaEjercicio>()
                .HasOne(ec => ec.Categoria)
                .WithMany(c => c.EjercicioCategorias)
                .HasForeignKey(ec => ec.CategoriaId);

            // Relación Ejercicio - RegistroProgreso
      
            modelBuilder.Entity<Ejercicio>()
                .HasMany(e => e.RegistrosProgreso)
                .WithOne(rp => rp.Ejercicios)
                .HasForeignKey(rp => rp.EjercicioId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
