using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCadastrador.Models;

namespace WebCadastrador.ViewModels
{
    public class ItemCarrinhoViewModel
    {
        public int Id { get; set; }
        public virtual Produto Produto { get; set; }
        public int Quantidade { get; set; } = 1;
        public decimal Preco { get; set; }
    }
}
