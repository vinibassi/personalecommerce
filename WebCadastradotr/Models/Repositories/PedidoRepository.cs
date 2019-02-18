using Microsoft.AspNetCore.Http;
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

        public void AddItem(ItemPedido p)
        {
            context.Add(p);
            context.SaveChanges();
        }

        public void CriaPedido(Pedido pedido)
        {
            context.Add(pedido);
            context.SaveChanges();
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
        void CriaPedido(Pedido pedido);
        Pedido FindById(int id);
        void AddItem(ItemPedido p);
    }
}
