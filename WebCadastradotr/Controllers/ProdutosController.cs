using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCadastrador.Models;

namespace WebCadastrador.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly WebCadastradorContext _context;

        public ProdutosController(WebCadastradorContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            var pDetailsViewModel = new ProdutoDetailsViewModel();
            pDetailsViewModel.Nome = produto.Nome;
            pDetailsViewModel.Preco = produto.Preco;
            pDetailsViewModel.Fabricante = produto.Fabricante;
            pDetailsViewModel.Id = produto.Id;

            return View(pDetailsViewModel);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewBag.Fabricantes = _context.Fabricante.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoCreateViewModel produtoCreateViewModel)
        {
            if (!produtoCreateViewModel.Preco.ToString().EndsWith("3"))
            {
                ModelState.AddModelError("Preco", "O preço deve terminar em 3.");
            }
            if (ModelState.IsValid)
            {
                var fabricante = _context.Fabricante.FirstOrDefault(p => p.Id == produtoCreateViewModel.Fabricante);
                var produto = new Produto
                {
                    Nome = produtoCreateViewModel.Nome,
                    Preco = produtoCreateViewModel.Preco,
                    Fabricante = fabricante
                };
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Fabricantes = _context.Fabricante.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            return View(produtoCreateViewModel);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var pEditViewModel = new ProdutoEditViewModel();
            var produto = await _context.Produto.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            if (produto == null)
            {
                return NotFound();
            }

            ViewBag.Fabricantes = _context.Fabricante.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

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
        [ValidateAntiForgeryToken]
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
                var fabricante = _context.Fabricante.FirstOrDefault(p => p.Id == produtoEditViewModel.FabricanteId);
                var produto = await _context.Produto.FindAsync(produtoEditViewModel.Id);
                if(produto == null)
                {
                    return NotFound();
                }
                produto.Nome = produtoEditViewModel.Nome;
                produto.Preco = produtoEditViewModel.Preco;
                produto.Fabricante = fabricante;

                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produtoEditViewModel.Id))
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

            ViewBag.Fabricantes = _context.Fabricante.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            return View(produtoEditViewModel);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        //// POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }
    }
}
