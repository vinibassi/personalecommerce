using System.Collections.Generic;
using System.Threading.Tasks;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.TestesDeProduto
{
    public class MockFabricanteRepository : IFabricanteRepository
    {
        public int Id;
        public Fabricante fabricante = new Fabricante();

        public Task<Fabricante> FindByIdAsync(int id)
        {
            Id = id;
            return Task.FromResult(fabricante);
        }

        public Task<List<Fabricante>> ListaFabricantesAsync()
        {
            return Task.FromResult(new List<Fabricante>{ new Fabricante()});
        }
    }
}