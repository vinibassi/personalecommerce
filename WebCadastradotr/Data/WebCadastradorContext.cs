using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WebCadastrador.Models
{
    public class WebCadastradorContext : DbContext
    {
        public WebCadastradorContext (DbContextOptions<WebCadastradorContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new EnumToNumberConverter<EstadoCivil, byte>();

            modelBuilder
                .Entity<Clientes>()
                .Property(e => e.EstadoCivil)
                .HasConversion(converter);
        }

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Fabricante> Fabricante { get; set; }

        public DbSet<Clientes> Clientes { get; set; }
    }
}
