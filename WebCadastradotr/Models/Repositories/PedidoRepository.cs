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
        private readonly HttpContextAccessor contextAccessor;

        public PedidoRepository(WebCadastradorContext context, HttpContextAccessor contextAccessor)
        {
            this.context = context;
            this.contextAccessor = contextAccessor;
        }

        //private void SetPedidoId(int pedidoId)
        //{
        //    contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        //}
        public void AddItem(ItemPedido p)
        {
            context.Add(p);
            context.SaveChanges();
        }

        public Pedido CriaPedido()
        {
            var pedido = new Pedido();
            context.Pedido.Add(pedido);
            context.SaveChanges();
            //return pedido;
            //var pedidoId = GetPedidoId();
            //var pedido = new Pedido();
            //pedido = context.Pedido.Include(p => p.Itens)
            //                       .ThenInclude(i => i.Produto)
            //                       .Where(p => p.Id == pedidoId)
            //                       .SingleOrDefault();
            if (pedido == null)
            {
                pedido = new Pedido();
                context.Add(pedido);
                context.SaveChanges();
                //SetPedidoId(pedido.Id);
            }
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
