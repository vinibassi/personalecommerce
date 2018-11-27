using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCadastrador.Models.Repositories
{
    public class ProdutoRepository : IProdutoRepositorio
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
    }

    public interface IProdutoRepositorio
    {
        Task UpdateAsync(Produto produto);
        Task AddAsync(Produto produto);
        Task<Produto> FindProdutoByIdAsync(int id);
        Task RemoveAsync(Produto produto);
    }
}
