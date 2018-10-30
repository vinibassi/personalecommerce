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
        [MaxLength(18, ErrorMessage = "O CNPJ do fabricante deve ter no máximo 18 caracteres.")]
        public string CNPJ { get; set; }
        [Required]
        public string Endereco { get; set; }
        public IList<Produto> Produtos { get; set; }

        public bool IsCnpj()
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            CNPJ = CNPJ.Trim();
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            if (CNPJ.Length != 14)
                return false;

            tempCnpj = CNPJ.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return CNPJ.EndsWith(digito);
        }
    }
}


