﻿namespace WebCadastrador.Models
{
  public class ProdutoDetailsVMUser
    {
        public int Id { get; set; }
        public Fabricante Fabricante { get;  set; }
        public string Nome { get;  set; }
        public decimal Preco { get;  set; }
        public string Url { get; set; }

    }
}