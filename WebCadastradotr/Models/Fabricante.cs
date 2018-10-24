using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCadastrador.Models
{
    public class Fabricante
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "O nome do produto deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [MinLength(14, ErrorMessage = "O CNPJ do fabricante deve ter no mínimo 14 caracteres.")]
        [MaxLength (14, ErrorMessage = "O CNPJ do fabricante deve ter no máximo 14 caracteres.")]
        public int CNPJ { get; set; }
        [Required]
        public string Endereco { get; set; }
    }
}
