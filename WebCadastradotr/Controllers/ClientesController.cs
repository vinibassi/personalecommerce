using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;
using WebCadastrador.Models.Validations;
using WebCadastrador.ViewModels;

namespace WebCadastrador.Controllers
{
    public class ClientesController : Controller
    {
        private readonly WebCadastradorContext _context;
        private IClienteRepository clienteRepository;

        public ClientesController(WebCadastradorContext context, IClienteRepository clienteRepository)
        {
            _context = context;
            this.clienteRepository = clienteRepository;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientesViewModel clientesViewModel)
        {
            var clientesValidator = new ClientesValidator();
            if (!clientesValidator.IsCpf(clientesViewModel.CPF))
            {
                ModelState.AddModelError("CPF", "O CPF é inválido.");
            }

            var cliente = new Clientes();
            cliente.Id = clientesViewModel.Id;
            cliente.Nome = clientesViewModel.Nome;
            cliente.Sobrenome = clientesViewModel.Sobrenome;
            cliente.CPF = clientesViewModel.CPF;
            cliente.Endereco = clientesViewModel.Endereco;
            cliente.Idade = clientesViewModel.Idade;
            cliente.EstadoCivil = clientesViewModel.estadoCivil;

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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientesViewModel clientesViewModel)
        {
            var cliente = new Clientes();
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
                cliente.Id = clientesViewModel.Id;
                cliente.Nome = clientesViewModel.Nome;
                cliente.Sobrenome = clientesViewModel.Sobrenome;
                cliente.CPF = clientesViewModel.CPF;
                cliente.Endereco = clientesViewModel.Endereco;
                cliente.Idade = clientesViewModel.Idade;
                cliente.EstadoCivil = clientesViewModel.estadoCivil;

                try
                {
                    await clienteRepository.UpdateClienteAsync(cliente);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(clientesViewModel.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ClientesViewModel clientesViewModel)
        {
            var cliente = await clienteRepository.FindClienteByIdAsync(clientesViewModel.Id);
            await clienteRepository.RemoveClienteAsync(cliente);
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
