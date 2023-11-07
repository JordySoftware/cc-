using AseguradoraViamatica.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AseguradoraViamatica
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SegurosAsegurados>().HasKey( x=> new {x.SeguroId, x.AseguradoId});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Seguro> Seguros { get; set; }
        public DbSet<Asegurado> Asegurados { get; set; }

        public DbSet<SegurosAsegurados> SegurosAsegurados { get; set; }


    }
}
