using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCadastrador.Data;

namespace WebCadastrador.Models.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private WebCadastradorContext context;

        public PedidoRepository(WebCadastradorContext context)
        {
            this.context = context;
        }

        public void AddItem(ItemPedido item)
        {
            var p = new ItemPedido
            {
                Id = item.Id,
                Pedido = item.Pedido,
                PrecoUnitario = item.PrecoUnitario,
                Produto = item.Produto,
                Quantidade = item.Quantidade
            };
            context.Add(p);
            context.SaveChanges();
        }

        public Pedido CriaPedido()
        {
            var pedido = new Pedido();
            context.Pedido.Add(pedido);
            context.SaveChanges();
            return pedido;
        }

        public Pedido FindById(int id)
        {
            var pedido = context.Pedido.Find(id);
            return pedido;
        }

        public List<ItemPedido> ItemList()
        {
            return context.ItemPedido.ToList();
        }
    }
    public interface IPedidoRepository
    {
        List<ItemPedido> ItemList();
        Pedido CriaPedido();
        Pedido FindById(int id);
        void AddItem(ItemPedido p);
    }
}
