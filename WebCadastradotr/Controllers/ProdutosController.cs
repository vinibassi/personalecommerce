using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace WebCadastrador.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository produtoRepositorio;
        private readonly IFabricanteRepository fabricanteRepository;

        public ProdutosController(IProdutoRepository produtoRepositorio, IFabricanteRepository fabricanteRepository)
        {
            this.produtoRepositorio = produtoRepositorio;
            this.fabricanteRepository = fabricanteRepository;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return View(await produtoRepositorio.ListaProdutosAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await produtoRepositorio.FindProdutoByIdAsync(id.Value);
            if (produto == null)
            {
                return NotFound();
            }

            var pDetailsViewModel = new ProdutoDetailsViewModel
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Fabricante = produto.Fabricante,
                Id = produto.Id
            };

            return View(pDetailsViewModel);
        }

        // GET: Produtos/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Fabricantes = (await fabricanteRepository.ListaFabricantesAsync())
                                                             .Select(c => new SelectListItem() { Text = c.Nome, Value = c.Id.ToString() })
                                                             .ToList();
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoCreateViewModel produtoCreateViewModel)
        {
            if (!produtoCreateViewModel.Preco.ToString().EndsWith("3"))
            {
                ModelState.AddModelError("Preco", "O preço deve terminar em 3.");
            }
            if (ModelState.IsValid)
            {
                var fabricante = await fabricanteRepository.FindByIdAsync(produtoCreateViewModel.Fabricante);
                var produto = new Produto
                {
                    Nome = produtoCreateViewModel.Nome,
                    Preco = produtoCreateViewModel.Preco,
                    Fabricante = fabricante
                };
                await produtoRepositorio.AddAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Fabricantes = (await fabricanteRepository.ListaFabricantesAsync())
                                                             .Select(c => new SelectListItem(){ Text = c.Nome, Value = c.Id.ToString() })
                                                             .ToList();

            return View(produtoCreateViewModel);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var pEditViewModel = new ProdutoEditViewModel();
            var produto = await produtoRepositorio.FindProdutoByIdAsync(id.Value);

            if (id == null)
            {
                return NotFound();
            }
            if (produto == null)
            {
                return NotFound();
            }

            ViewBag.Fabricantes = (await fabricanteRepository.ListaFabricantesAsync()).Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() });

            pEditViewModel.Nome = produto.Nome;
            pEditViewModel.Preco = produto.Preco;
            pEditViewModel.FabricanteId = produto.Fabricante.Id;
            pEditViewModel.Id = produto.Id;

            return View(pEditViewModel);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoEditViewModel produtoEditViewModel)
        {
            if (produtoEditViewModel.Id == 0)
            {
                return NotFound();
            }
            if (!produtoEditViewModel.Preco.ToString().EndsWith("3"))
            {
                ModelState.AddModelError("Preco", "O preço deve terminar em 3.");
            }

            if (ModelState.IsValid)
            {
                var fabricante = await fabricanteRepository.FindByIdAsync(produtoEditViewModel.FabricanteId);
                var produto = await produtoRepositorio.FindProdutoByIdAsync(produtoEditViewModel.Id); 
                if (produto == null)
                {
                    return NotFound();
                }
                produto.Nome = produtoEditViewModel.Nome;
                produto.Preco = produtoEditViewModel.Preco;
                produto.Fabricante = fabricante;

                try
                {
                    await produtoRepositorio.UpdateAsync(produto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await produtoRepositorio.ProdutoExists(produtoEditViewModel.Id))
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

            await fabricanteRepository.ListaFabricantesAsync();

            return View(produtoEditViewModel);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await produtoRepositorio.FindProdutoByIdAsync(id.Value);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        //// POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await produtoRepositorio.FindProdutoByIdAsync(id);
            await produtoRepositorio.RemoveAsync(produto);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProdutoExists(int id)
        {
            var produto = await produtoRepositorio.ProdutoExists(id);
            return produto;
        }
    }
}
