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

        public async Task AddFabricanteAsync(Fabricante fabricante)
        {
            context.Add(fabricante);
            await context.SaveChangesAsync();
        }

        public async Task<(bool Exists, IDictionary<string, string> Errors)> ExistsAsync(Fabricante fabricante)
        {
            var existeCNPJ = await context.Fabricante.AnyAsync(r => r.CNPJ == fabricante.CNPJ);
            if (existeCNPJ)
                return (true, new Dictionary<string, string> { {"CNPJ", "Este CNPJ já está cadastrado." } });
            return (false, new Dictionary<string, string>());
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

        public async Task RemoveFabricanteAsync(Fabricante fabricante)
        {
            context.Fabricante.Remove(fabricante);
            await context.SaveChangesAsync();
        }

        public async Task UpdateFabricanteAsync(Fabricante fabricante)
        {
            context.Update(fabricante);
            await context.SaveChangesAsync();
        }
    }

    public interface IFabricanteRepository
    {
        Task UpdateFabricanteAsync(Fabricante fabricante);
        Task RemoveFabricanteAsync(Fabricante fabricante);
        Task<(bool Exists, IDictionary<string, string> Errors)> ExistsAsync(Fabricante fabricante);
        Task AddFabricanteAsync(Fabricante fabricante);
        Task<Fabricante> FindByIdAsync(int id);
        Task<List<Fabricante>> ListaFabricantesAsync();
    }
}
