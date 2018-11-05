using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCadastrador.Models
{
    public class ProdutoEditViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int FabricanteId { get; set; }
        public decimal Preco { get; set; }
    }
}
