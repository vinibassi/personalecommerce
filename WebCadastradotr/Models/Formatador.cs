using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCadastrador.Models
{
    public class Formatador
    {
        public static string CNPJ(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;
            if (texto.Length != 14)
                return texto;
                    
            return $"{texto.Substring(0,2)}.{texto.Substring(2, 3)}.{texto.Substring(5,3)}/{texto.Substring(8,4)}-{texto.Substring(12,2)}";
        }
    }
}
