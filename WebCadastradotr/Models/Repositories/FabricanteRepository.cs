using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace WebCadastrador.Models.Repositories
{
    public class FabricanteRepository : IFabricanteRepository
    {
        private readonly WebCadastradorContext context;
        public FabricanteRepository(WebCadastradorContext context)
        {
            this.context = context;
        }

        public async Task<Fabricante> FindByIdAsync(int id)
        {
            var fabricante = await context.Fabricante.FirstOrDefaultAsync(p => p.Id == id);
            return fabricante;
        }

        public Task<List<Fabricante>> ListaFabricantesAsync()
        {
           return context.Fabricante.ToListAsync();
        }
    }

    public interface IFabricanteRepository
    {
        Task<Fabricante> FindByIdAsync(int id);

        Task<List<Fabricante>> ListaFabricantesAsync();
    }
}
