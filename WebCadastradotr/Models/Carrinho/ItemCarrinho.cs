using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCadastrador.Models.Carrinho
{
    public class ItemCarrinho
    {
        public int Id { get; set; }
        public virtual Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
