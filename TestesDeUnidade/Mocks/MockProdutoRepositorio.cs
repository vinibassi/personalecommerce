using System.Threading.Tasks;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.Mocks
{
    public class MockProdutoRepositorio : IProdutoRepositorio
    {
        public Produto Produto { get; private set; } = new Produto();
        public bool AddAsyncFoiChamado { get; set; }
        public bool RemoveAsyncFoiChamado { get; set; }
        public bool FindProdutoFoiChamado { get; set; }
        public bool UpdateAsyncFoiChamado { get; set; }

        public Task AddAsync(Produto produto)
        {
            AddAsyncFoiChamado = true;
            Produto = produto;
            return Task.CompletedTask;
        }

        public Task<Produto> FindProdutoByIdAsync(int id)
        {
            FindProdutoFoiChamado = true;
            Produto.Id = id;
            return Task.FromResult(Produto);
        }

        public Task RemoveAsync(Produto produto)
        {
            RemoveAsyncFoiChamado = true;
            Produto = produto;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Produto produto)
        {
            UpdateAsyncFoiChamado = true;
            Produto = produto;
            return Task.CompletedTask;
        }
    }
}