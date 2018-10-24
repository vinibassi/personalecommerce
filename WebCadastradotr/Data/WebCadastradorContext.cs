using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCadastrador.Models;

namespace WebCadastrador.Models
{
    public class WebCadastradorContext : DbContext
    {
        public WebCadastradorContext (DbContextOptions<WebCadastradorContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }

        public DbSet<WebCadastrador.Models.Fabricante> Fabricante { get; set; }
    }
}
