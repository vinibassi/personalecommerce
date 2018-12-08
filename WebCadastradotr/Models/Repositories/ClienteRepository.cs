using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebCadastrador.Models.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly WebCadastradorContext context;
        public ClienteRepository(WebCadastradorContext context)
        {
            this.context = context;
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            context.Add(cliente);
            await context.SaveChangesAsync();
        }

        public async Task<(bool Exists, IDictionary<string, string> Errors)> ExistsAsync(Cliente cliente)
        {
            var existeCPF = await context.Clientes.AnyAsync(r => r.CPF == cliente.CPF && r.Id != cliente.Id);
            if (existeCPF)
                return (true, new Dictionary<string, string> { { "CPF", "Este CPF já está cadastrado." } });
            return (false, new Dictionary<string, string>());
        }

        public async Task<Cliente> FindClienteByIdAsync(int id)
        {
            var clientes = await context.Clientes.FindAsync(id);
            return clientes;
        }

        public async Task RemoveClienteAsync(Cliente cliente)
        {
            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            context.Update(cliente);
            await context.SaveChangesAsync();
        }
        public async Task<bool> ClientesExists(int id)
        {
            var clienteExists =  await context.Clientes.AnyAsync(e => e.Id == id);
            return clienteExists;
        }
        public Task<List<Cliente>> ListaClientesAsync()
        {
            return context.Clientes.ToListAsync();
        }
    }
    
    public interface IClienteRepository
    {
        Task<List<Cliente>> ListaClientesAsync();
        Task RemoveClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task<Cliente> FindClienteByIdAsync(int id);
        Task AddClienteAsync(Cliente cliente);
        Task<bool> ClientesExists(int id);
        Task<(bool Exists, IDictionary<string, string> Errors)> ExistsAsync(Cliente cliente);
    }
}
