using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCadastrador.Data;

namespace WebCadastrador.Models.Repositories
{
    public class FabricanteRepository : IFabricanteRepository
    {
        private readonly WebCadastradorContext context;
        public FabricanteRepository(WebCadastradorContext context)
        {
            this.context = context;
        }

        public virtual async Task AddFabricanteAsync(Fabricante fabricante)
        {
            context.Add(fabricante);
            await context.SaveChangesAsync();
        }

        public virtual async Task<(bool Exists, IDictionary<string, string> Errors)> ExistsAsync(Fabricante fabricante)
        {
            var existeCNPJ = await context.Fabricante.AnyAsync(r => r.CNPJ == fabricante.CNPJ && r.Id != fabricante.Id);
            if (existeCNPJ)
                return (true, new Dictionary<string, string> { {"CNPJ", "Este CNPJ já está cadastrado." } });
            return (false, new Dictionary<string, string>());
        }

        public virtual async Task<bool> FabricanteExists(int id)
        {
            var fabricanteExiste = await context.Fabricante.AnyAsync(e => e.Id == id);
            return fabricanteExiste;
        }

        public virtual async Task<Fabricante> FindByIdAsync(int id)
        {
            var fabricante = await context.Fabricante.FirstOrDefaultAsync(p => p.Id == id);
            return fabricante;
        }

        public virtual Task<List<Fabricante>> ListaFabricantesAsync()
        {
            return context.Fabricante.ToListAsync();
        }

        public virtual async Task RemoveFabricanteAsync(Fabricante fabricante)
        {
            context.Fabricante.Remove(fabricante);
            await context.SaveChangesAsync();
        }

        public virtual async Task UpdateFabricanteAsync(Fabricante fabricante)
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
        Task<bool> FabricanteExists(int id);
        Task<List<Fabricante>> ListaFabricantesAsync();
    }
}
