using System.Collections.Generic;
using System.Threading.Tasks;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.Mocks
{
    public class MockFabricanteRepository : IFabricanteRepository
    {
        public int Id;
        public Fabricante Fabricante = new Fabricante();
        public bool ListaFoiChamada;
        public bool AddFabricanteFoiChamado;
        public bool RemoveFabricanteFoiChamado;
        public bool FindFabricanteFoiChamado;
        public string cnpj;
        public bool UpdateFoiChamado;

        public Task AddFabricanteAsync(Fabricante fabricante)
        {
            AddFabricanteFoiChamado = true;
            Fabricante = fabricante;
            return Task.CompletedTask;
        }

        public Task<(bool Exists, IDictionary<string, string> Errors)> ExistsAsync(Fabricante fabricante)
        {
            return Task.FromResult<(bool, IDictionary<string,string>)>((false, new Dictionary<string, string>()));
        }

        public Task<Fabricante> FindByIdAsync(int id)
        {
            FindFabricanteFoiChamado = true;
            Id = id;
            return Task.FromResult(Fabricante);
        }

        public Task<List<Fabricante>> ListaFabricantesAsync()
        {
            ListaFoiChamada = true ;
            return Task.FromResult(new List<Fabricante>{ new Fabricante()});
        }

        public Task RemoveFabricanteAsync(Fabricante fabricante)
        {
            RemoveFabricanteFoiChamado = true;
            Fabricante = fabricante;
            return Task.CompletedTask;
        }

        public Task UpdateFabricanteAsync(Fabricante fabricante)
        {
            UpdateFoiChamado = true;
            Fabricante = fabricante;
            return Task.CompletedTask;
        }
    }
}