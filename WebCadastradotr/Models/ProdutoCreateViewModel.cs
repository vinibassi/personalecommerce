using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCadastrador.Models
{
    public class ProdutoCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "O nome do produto deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }
        [Required]
        public int Fabricante { get; set; }
        [Required]
        public decimal Preco { get; set; }
    }
}
