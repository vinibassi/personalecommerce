using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCadastrador.Models.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly WebCadastradorContext context;

        public ProdutoRepository(WebCadastradorContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Produto produto)
        {
            context.Add(produto);
            await context.SaveChangesAsync();
        }

        public async Task<Produto> FindProdutoByIdAsync(int id)
        {
            var produto = await context.Produto.FindAsync(id);
            return produto;
        }

        public async Task RemoveAsync(Produto produto)
        {
           context.Produto.Remove(produto);
           await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produto produto)
        {
            context.Update(produto);
            await context.SaveChangesAsync();
        }
        public async Task<bool> ProdutoExists(int id)
        {
            var p = await context.Produto.AnyAsync(e => e.Id == id);
            return p;
        }

        public Task<List<Produto>> ListaProdutosAsync()
        {
            return context.Produto.ToListAsync();
        }
    }

    public interface IProdutoRepository
    {
        Task<bool> ProdutoExists(int id);
        Task UpdateAsync(Produto produto);
        Task AddAsync(Produto produto);
        Task<Produto> FindProdutoByIdAsync(int id);
        Task RemoveAsync(Produto produto);
        Task<List<Produto>> ListaProdutosAsync();
    }
}
