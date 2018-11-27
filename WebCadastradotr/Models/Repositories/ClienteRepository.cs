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

        public async Task AddClienteAsync(Clientes cliente)
        {
            context.Add(cliente);
            await context.SaveChangesAsync();
        }

        public async Task<(bool Exists, IDictionary<string, string> Errors)> ExistsAsync(Clientes cliente)
        {
            var existeCPF = await context.Clientes.AnyAsync(r => r.CPF == cliente.CPF);
            if (existeCPF)
                return (true, new Dictionary<string, string> { { "CPF", "Este CPF já está cadastrado." } });
            return (false, new Dictionary<string, string>());
        }

        public async Task<Clientes> FindClienteByIdAsync(int id)
        {
            var clientes = await context.Clientes.FindAsync(id);
            return clientes;
        }

        public async Task RemoveClienteAsync(Clientes cliente)
        {
            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(Clientes cliente)
        {
            context.Update(cliente);
            await context.SaveChangesAsync();
        }
    }

    public interface IClienteRepository
    {
        Task RemoveClienteAsync(Clientes cliente);
        Task UpdateClienteAsync(Clientes cliente);
        Task<Clientes> FindClienteByIdAsync(int id);
        Task AddClienteAsync(Clientes cliente);
        Task<(bool Exists, IDictionary<string, string> Errors)> ExistsAsync(Clientes cliente);
    }
}
