﻿using System.Collections.Generic;

namespace WebCadastrador.Models
{
    public  class Pedido
    {
        public int Id { get; set; }
        public virtual List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
        public void AdicionarItens(List<(Produto produto, int quantidade)> produtosEQuantidades)
        {
            foreach (var (p, q) in produtosEQuantidades)
            {
                AdicionarItem(p, q);
            }
        }
        public void AdicionarItem(Produto produto, int quantidade)
        {
            var itemPedido = new ItemPedido()
            {
                Pedido = this,
                PrecoUnitario = produto.Preco,
                Produto = produto,
                Quantidade = quantidade
            };
            Itens.Add(itemPedido);
        }
    }
    public class ItemPedido
    {
        public int Id { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

    }
}
