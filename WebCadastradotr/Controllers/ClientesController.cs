using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;
using WebCadastrador.Models.Validations;
using WebCadastrador.ViewModels;

namespace WebCadastrador.Controllers
{
    public class ClientesController : Controller
    {
        private IClienteRepository clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await clienteRepository.ListaClientesAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await clienteRepository.FindClienteByIdAsync(id.Value);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // GET: Clientes/Create
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create(ClientesViewModel clientesViewModel)
        {
            var clientesValidator = new ClientesValidator();
            if (!clientesValidator.IsCpf(clientesViewModel.CPF))
            {
                ModelState.AddModelError("CPF", "O CPF é inválido.");
            }

            var cliente = new Cliente
            {
                Id = clientesViewModel.Id,
                Nome = clientesViewModel.Nome,
                Sobrenome = clientesViewModel.Sobrenome,
                CPF = clientesViewModel.CPF,
                Endereco = clientesViewModel.Endereco,
                Idade = clientesViewModel.Idade,
                EstadoCivil = clientesViewModel.estadoCivil
            };

            var (exists, errorExists) = await clienteRepository.ExistsAsync(cliente);
            if (exists)
            {
                foreach (var error in errorExists)
                    ModelState.AddModelError(error.Key, error.Value);
            }
            if (ModelState.IsValid)
            {
                await clienteRepository.AddClienteAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(clientesViewModel);
        }

        // GET: Clientes/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await clienteRepository.FindClienteByIdAsync(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }
            var clienteVM = new ClientesViewModel
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                CPF = cliente.CPF,
                Endereco = cliente.Endereco,
                Idade = cliente.Idade,
                estadoCivil = cliente.EstadoCivil
            };
            return View(clienteVM);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(ClientesViewModel clientesViewModel)
        {
            var cliente = new Cliente
            {
                Id = clientesViewModel.Id,
                Nome = clientesViewModel.Nome,
                Sobrenome = clientesViewModel.Sobrenome,
                CPF = clientesViewModel.CPF,
                Endereco = clientesViewModel.Endereco,
                Idade = clientesViewModel.Idade,
                EstadoCivil = clientesViewModel.estadoCivil
            };
            var clientesValidator = new ClientesValidator();
            if (!clientesValidator.IsCpf(clientesViewModel.CPF))
            {
                ModelState.AddModelError("CPF", "O CPF é inválido.");
            }
            var (exists, errorExists) = await clienteRepository.ExistsAsync(cliente);
            if (exists)
            {
                foreach (var error in errorExists)
                    ModelState.AddModelError(error.Key, error.Value);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await clienteRepository.UpdateClienteAsync(cliente);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await clienteRepository.ClientesExists(clientesViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clientesViewModel);
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await clienteRepository.FindClienteByIdAsync(id.Value);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ClientesViewModel clientesViewModel)
        {
            var cliente = await clienteRepository.FindClienteByIdAsync(clientesViewModel.Id);
            await clienteRepository.RemoveClienteAsync(cliente);
            return RedirectToAction(nameof(Index));
        }
    }
}
