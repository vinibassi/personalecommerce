using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.Mocks
{
    class MockClienteRepository : IClienteRepository
    {
        public bool UpdateClienteFoiChamado;
        public Clientes Cliente = new Clientes();
        public bool AddClienteFoiChamado;
        public bool FindClienteFoiChamado;
        public bool RemoveClienteFoiChamado;
        private int Id;

        public Task AddClienteAsync(Clientes cliente)
        {
            AddClienteFoiChamado = true;
            Cliente = cliente;
            return Task.CompletedTask;
        }

        public Task<(bool Exists, IDictionary<string, string> Errors)> ExistsAsync(Clientes cliente)
        {
            return Task.FromResult<(bool, IDictionary<string, string>)>((false, new Dictionary<string, string>()));
        }

        public Task<Clientes> FindClienteByIdAsync(int id)
        {
            FindClienteFoiChamado = true;
            Id = id;
            return Task.FromResult(Cliente);
        }

        public Task RemoveClienteAsync(Clientes cliente)
        {
            RemoveClienteFoiChamado = true;
            Cliente = cliente;
            return Task.CompletedTask;
        }

        public Task UpdateClienteAsync(Clientes cliente)
        {
            UpdateClienteFoiChamado = true;
            Cliente = cliente;
            return Task.CompletedTask;
        }
    }
}
