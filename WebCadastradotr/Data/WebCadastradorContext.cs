using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCadastrador.Areas.Identity.Data;
using WebCadastrador.Models;

namespace WebCadastrador.Data
{
    public class WebCadastradorContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public WebCadastradorContext (DbContextOptions<WebCadastradorContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var converter = new EnumToNumberConverter<EstadoCivil, byte>();

            modelBuilder
                .Entity<Cliente>()
                .Property(e => e.EstadoCivil)
                .HasConversion(converter);
        }

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Fabricante> Fabricante { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
