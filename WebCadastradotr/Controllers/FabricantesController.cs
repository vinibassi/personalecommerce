using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;
using WebCadastrador.Models.Validations;
using WebCadastrador.ViewModels;
using WebCadastradotr;

namespace WebCadastrador.Controllers
{
    public class FabricantesController : Controller
    {
        private readonly IProdutoRepository produtoRepositorio;
        private readonly IFabricanteRepository fabricanteRepository;

        public FabricantesController(IProdutoRepository produtoRepositorio, IFabricanteRepository fabricanteRepository)
        {
            this.produtoRepositorio = produtoRepositorio;
            this.fabricanteRepository = fabricanteRepository;
        }


        // GET: Fabricantes
        [Authorize(nameof(AuthPolicies.ViewOnly))]
        public async Task<IActionResult> Index()
        {
            return View(await fabricanteRepository.ListaFabricantesAsync());
        }

        // GET: Fabricantes/Details/5
        [Authorize(nameof(AuthPolicies.ViewOnly))]
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
        [Authorize(nameof(AuthPolicies.EditAndCreate))]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fabricantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(nameof(AuthPolicies.EditAndCreate))]
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
        [Authorize(nameof(AuthPolicies.EditAndCreate))]
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
            var fabricanteVM = new FabricantesViewModel
            {
                Id = fabricante.Id,
                Nome = fabricante.Nome,
                CNPJ = fabricante.CNPJ,
                Endereco = fabricante.Endereco,
            };
            return View(fabricanteVM);
        }

        // POST: Fabricantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(nameof(AuthPolicies.EditAndCreate))]
        public async Task<IActionResult> Edit(FabricantesViewModel fabricanteViewModel)
        {
            var fabricante = new Fabricante
            {
                Nome = fabricanteViewModel.Nome,
                CNPJ = fabricanteViewModel.CNPJ,
                Endereco = fabricanteViewModel.Endereco,
                Id = fabricanteViewModel.Id
            };

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
        [Authorize(nameof(AuthPolicies.Delete))]
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
        [Authorize(nameof(AuthPolicies.Delete))]
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
