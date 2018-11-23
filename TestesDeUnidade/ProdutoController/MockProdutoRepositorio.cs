using System.Threading.Tasks;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.TestesDeProduto
{
    public class MockProdutoRepositorio : IProdutoRepositorio
    {
        public Produto Produto { get; private set; }
        public bool AddAsyncFoiChamado { get; set; }

        public Task AddAsync(Produto produto)
        {
            AddAsyncFoiChamado = true;
            Produto = produto;
            return Task.CompletedTask;
        }
    }
}