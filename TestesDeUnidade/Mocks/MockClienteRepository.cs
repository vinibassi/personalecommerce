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
        public int Id;
        public bool ListaFoiChamada;

        public Task AddClienteAsync(Clientes cliente)
        {
            AddClienteFoiChamado = true;
            Cliente = cliente;
            return Task.CompletedTask;
        }

        public Task<bool> ClientesExists(int id)
        {
            Id = id;
            return Task.FromResult<bool>(true);
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

        public Task<List<Clientes>> ListaClientesAsync()
        {
            ListaFoiChamada = true;
            return Task.FromResult(new List<Clientes> { new Clientes() });
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
