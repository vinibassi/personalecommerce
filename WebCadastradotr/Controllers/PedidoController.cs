using Microsoft.AspNetCore.Mvc;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace WebCadastrador.Controllers
{
    public class PedidoController : Controller
    {
        private IPedidoRepository pedidoRepository;
        public PedidoController(IPedidoRepository pedidoRepository)
        {
            this.pedidoRepository = pedidoRepository;
        }
        
        public IActionResult Carrinho(ItemPedido ip)
        {
            var pedido = pedidoRepository.CriaPedido();
            pedido = pedidoRepository.FindById(pedido.Id);
            pedidoRepository.AddItem(ip);
            return View(pedido.Itens);
        }
    }
}