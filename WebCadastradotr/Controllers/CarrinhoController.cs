using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCadastrador.Models.Carrinho;
using WebCadastrador.Models.Repositories;

namespace WebCadastrador.Controllers
{
    public class CarrinhoController : Controller
    {
        private IProdutoRepository produtoRepository;

        public CarrinhoController(IProdutoRepository produtoRepository) => this.produtoRepository = produtoRepository;

        public async Task<ActionResult> Index()
        {
            var idProdutosDoCarrinho = HttpContext.Session.Get<List<int>>("carrinho");
            if (idProdutosDoCarrinho == null)
                return View(new Carrinho());
            var produtos = await Task.WhenAll(idProdutosDoCarrinho.Select(id => produtoRepository.FindProdutoByIdAsync(id)));
            var itens = produtos.Select(produto =>
                new ItemCarrinho()
                {
                    Preco = produto.Preco,
                    Quantidade = 1,
                    Produto = produto
                });
            return View(new Carrinho { Produtos = itens });
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var carrinho = HttpContext.Session.Get<List<int>>("carrinho") ?? new List<int>();
            carrinho.Add(id);
            HttpContext.Session.Set("carrinho", carrinho);
            return RedirectToAction(nameof(Index));
        }
    }
}