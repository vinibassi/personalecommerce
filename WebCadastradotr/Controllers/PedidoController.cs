//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using WebCadastrador.Models;
//using WebCadastrador.Models.Repositories;

//namespace WebCadastrador.Controllers
//{
//    public class PedidoController : Controller
//    {
//        private IPedidoRepository pedidoRepository;
//        private IProdutoRepository produtoRepository;

//        public PedidoController(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
//        {
//            this.pedidoRepository = pedidoRepository;
//            this.produtoRepository = produtoRepository;
//        }

//        public async Task<IActionResult> Carrinho(int Id)
//        {
//            HttpContext.Session.Set<Carrinho>("carrinho", carrinho);
//            var produto = await produtoRepository.FindProdutoByIdAsync(Id);
//            //var idPedido = HttpContext.Session.GetInt32("pedidoId");
//            var pedido = pedidoRepository.CriaPedido();
//            pedido = pedidoRepository.FindById(pedido.Id);
//            var itemPedido = new ItemPedido
//            {
//                Pedido = pedido,
//                PrecoUnitario = produto.Preco,
//                Produto = produto,
//                Quantidade = +1
//            };
//            pedidoRepository.AddItem(itemPedido);
//            return View(pedido.Itens);
//        }
//    }
//}