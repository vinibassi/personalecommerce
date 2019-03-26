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
using WebCadastradotr;

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
            var itensCarrinho = await Task.WhenAll(Itens.Select(async itemCarrinho =>
            {
                var produto = await produtoRepository.FindProdutoByIdAsync(itemCarrinho.ProdutoId);
                return new ItemCarrinhoViewModel()
                {
                    Preco = produto.Preco,
                    Produto = produto,
                    Quantidade = itemCarrinho.Quantidade
                };
            }));
            return View(new CarrinhoViewModel { Produtos = itensCarrinho });
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var carrinho = Itens;
            var item = carrinho.FirstOrDefault(itemCarrinho => itemCarrinho.ProdutoId == id);
            if (item == null)
                carrinho.Add(new ItemCarrinho { ProdutoId = id, Quantidade = 1 });
            else
                ++item.Quantidade;
            Itens = carrinho;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddItemButton(int id)
        {
            var carrinho = Itens;
            var item = carrinho.FirstOrDefault(itemCarrinho => itemCarrinho.ProdutoId == id);
            item.Quantidade++;
            Itens = carrinho;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveItemButton(int id)
        {
            var carrinho = Itens;
            var item = carrinho.FirstOrDefault(itemCarrinho => itemCarrinho.ProdutoId == id);
            item.Quantidade--;
            Itens = carrinho;
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> FinalizarPedido()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var itensDoCarrinho = Itens;
            var produtosEQuantidades = await Task.WhenAll(itensDoCarrinho.Select(async itemDoCarrinho => (await produtoRepository.FindProdutoByIdAsync(itemDoCarrinho.ProdutoId), itemDoCarrinho.Quantidade)));
            var pedido = new Pedido();
            pedidoRepository.CriaPedido(pedido);
            pedido.AdicionarItens(produtosEQuantidades.ToList());
            pedido = pedidoRepository.FindById(pedido.Id);
            return View((pedido, user));
        }

        [Authorize(nameof(AuthPolicies.Delete))]
        public async Task<IActionResult> PedidosRelatorio(Pedido p)
        {
            var itensDoCarrinho = Itens;
            var pedido = p;
            var produtosEQuantidades = await Task.WhenAll(itensDoCarrinho.Select(async itemDoCarrinho => (await produtoRepository.FindProdutoByIdAsync(itemDoCarrinho.ProdutoId), itemDoCarrinho.Quantidade)));
            pedido.AdicionarItens(produtosEQuantidades.ToList());

            return View(pedido);
        }


    }
}