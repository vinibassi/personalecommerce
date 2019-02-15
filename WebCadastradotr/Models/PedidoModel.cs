﻿using System.Collections.Generic;

namespace WebCadastrador.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public virtual List<ItemPedido> Itens { get; set; } /*= new List<ItemPedido>();*/
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