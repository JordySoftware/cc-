using Consultorio.Model;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Data
{
    public class ConsultorioDBContext : DbContext
    {
        public ConsultorioDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<Consulta> Consultas { get; set; }

        public DbSet<Perfil> Perfiles { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
