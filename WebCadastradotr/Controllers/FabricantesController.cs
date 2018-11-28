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
    public class FabricantesController : Controller
    {
        private readonly IProdutoRepositorio produtoRepositorio;
        private readonly IFabricanteRepository fabricanteRepository;

        public FabricantesController(IProdutoRepositorio produtoRepositorio, IFabricanteRepository fabricanteRepository)
        {
            this.produtoRepositorio = produtoRepositorio;
            this.fabricanteRepository = fabricanteRepository;
        }

        // GET: Fabricantes
        public async Task<IActionResult> Index()
        {
            return View(await fabricanteRepository.ListaFabricantesAsync());
        }

        // GET: Fabricantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fabricante = await fabricanteRepository.FindByIdAsync(id.Value);
            if (fabricante == null)
            {
                return NotFound();
            }
            var produtos = fabricante.Produtos;
            return View(fabricante);
        }

        // GET: Fabricantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fabricantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FabricantesViewModel fabricanteViewModel)
        {
            var fabricanteValidator = new FabricanteValidator();
            if (!fabricanteValidator.IsCnpj(fabricanteViewModel.CNPJ))
            {
                ModelState.AddModelError("CNPJ", "O CNPJ é inválido.");
            }
            var fabricante = new Fabricante();
            fabricante.Nome = fabricanteViewModel.Nome;
            fabricante.CNPJ = fabricanteViewModel.CNPJ;
            fabricante.Endereco = fabricanteViewModel.Endereco;
            var (exists, errorExists) = await fabricanteRepository.ExistsAsync(fabricante);
            if (exists)
            {
                foreach (var error in errorExists)
                    ModelState.AddModelError(error.Key, error.Value);
            }
            if (ModelState.IsValid)
            {
                await fabricanteRepository.AddFabricanteAsync(fabricante);
                return RedirectToAction(nameof(Index));
            }
            return View(fabricanteViewModel);
        }

        // GET: Fabricantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricante = await fabricanteRepository.FindByIdAsync(id.Value);
            if (fabricante == null)
            {
                return NotFound();
            }
            return View(fabricante);
        }

        // POST: Fabricantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FabricantesViewModel fabricanteViewModel)
        {
            var fabricante = new Fabricante();
            var fabricanteValidator = new FabricanteValidator();
            if (!fabricanteValidator.IsCnpj(fabricanteViewModel.CNPJ))
            {
                ModelState.AddModelError("CNPJ", "O CNPJ é inválido.");
            }
            var (exists, errorExists) = await fabricanteRepository.ExistsAsync(fabricante);
            if (exists)
            {
                foreach (var error in errorExists)
                    ModelState.AddModelError(error.Key, error.Value);
            }
            if (ModelState.IsValid)
            {
                fabricante.Nome = fabricanteViewModel.Nome;
                fabricante.CNPJ = fabricanteViewModel.CNPJ;
                fabricante.Endereco = fabricanteViewModel.Endereco;
                fabricante.Id = fabricanteViewModel.Id;

                try
                {
                    await fabricanteRepository.UpdateFabricanteAsync(fabricante);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await fabricanteRepository.FabricanteExists(fabricanteViewModel.Id))
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
            return View(fabricanteViewModel);
        }

        // GET: Fabricantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricante = await fabricanteRepository.FindByIdAsync(id.Value);
            if (fabricante == null)
            {
                return NotFound();
            }

            return View(fabricante);
        }

        // POST: Fabricantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fabricante = await fabricanteRepository.FindByIdAsync(id);
            await fabricanteRepository.RemoveFabricanteAsync(fabricante);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FabricanteExists(int id)
        {
            var fabricanteExiste = await fabricanteRepository.FabricanteExists(id);
            return fabricanteExiste;
        }
    }
}
