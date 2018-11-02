using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCadastrador.Models;
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
                .Property(e => e.Estado_Civil)
                .HasConversion(converter);
        }

        public DbSet<Produto> Produto { get; set; }

        public DbSet<WebCadastrador.Models.Fabricante> Fabricante { get; set; }

        public DbSet<WebCadastrador.Models.Clientes> Clientes { get; set; }
    }
}
