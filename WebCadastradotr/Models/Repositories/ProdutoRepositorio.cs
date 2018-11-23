using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCadastrador.Models.Repositories
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly WebCadastradorContext context;

        public ProdutoRepositorio(WebCadastradorContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Produto produto)
        {
            context.Add(produto);
            await context.SaveChangesAsync();
        }
    }

    public interface IProdutoRepositorio
    {
        Task AddAsync(Produto produto);
    }
}
