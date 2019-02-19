using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCadastrador.Areas.Identity.Data;
using WebCadastrador.Models;
using WebCadastrador.Models.Carrinho;
using WebCadastrador.Models.Repositories;
using WebCadastrador.ViewModels;

namespace WebCadastrador.Controllers
{
    public class CarrinhoController : Controller
    {
        private IProdutoRepository produtoRepository;
        private IPedidoRepository pedidoRepository;
        private UserManager<AppUser> userManager;

        public List<ItemCarrinho> Itens
        {
            get => HttpContext.Session.Get<List<ItemCarrinho>>("carrinho") ?? new List<ItemCarrinho>();
            set => HttpContext.Session.Set("carrinho", value);
        }

        public CarrinhoController(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository, UserManager<AppUser> userManager)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            if (!Itens.Any())
                return View(new CarrinhoViewModel());
            var produtos = await Task.WhenAll(Itens.Select(itemCarrinho => produtoRepository.FindProdutoByIdAsync(itemCarrinho.ProdutoId)));
            var itens = produtos.Select(produto =>
                new ItemCarrinhoViewModel()
                {
                    Preco = produto.Preco,
                    Produto = produto,
                    Quantidade = 1
                });
            return View(new CarrinhoViewModel { Produtos = itens });
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var carrinho = Itens;
            carrinho.Add(new ItemCarrinho { ProdutoId = id, Quantidade = 1 });
            Itens = carrinho;
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> FinalizarPedido()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var itensDoCarrinho = Itens;
            var produtos = await Task.WhenAll(itensDoCarrinho.Select(itemDoCarrinho => produtoRepository.FindProdutoByIdAsync(itemDoCarrinho.ProdutoId)));
            var pedido = new Pedido();
            pedidoRepository.CriaPedido(pedido);
            pedido.AdicionarItens(produtos.ToList());
            pedido = pedidoRepository.FindById(pedido.Id);
            return View((pedido, user));
        }


    }
}