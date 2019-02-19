using System.Collections.Generic;

namespace WebCadastrador.ViewModels
{
    public class CarrinhoViewModel
    {
        public virtual IEnumerable<ItemCarrinhoViewModel> Produtos { get; set; } = new ItemCarrinhoViewModel[0];
    }
}
