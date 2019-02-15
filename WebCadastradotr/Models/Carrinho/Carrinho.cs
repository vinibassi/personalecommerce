using System.Collections.Generic;

namespace WebCadastrador.Models.Carrinho
{
    public class Carrinho
    {
        public virtual IEnumerable<ItemCarrinho> Produtos { get; set; } = new ItemCarrinho[0];
    }
}
