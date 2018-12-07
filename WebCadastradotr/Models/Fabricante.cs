using System.Collections.Generic;

namespace WebCadastrador.Models
{
    public class Fabricante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public virtual IList<Produto> Produtos { get; set; }
    }
}


