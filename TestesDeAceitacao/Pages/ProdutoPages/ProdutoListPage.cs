using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages.ProdutoPages
{
    class ProdutoListPage
    {
        public List<ProdutoCadastrado> Produtos
        {
            get
            {
                var produtosCadastrados = new List<ProdutoCadastrado>();
                var linhas = SetupGlobal.Driver.FindElements(By.CssSelector("body > div > table > tbody > tr"));
                foreach (var linha in linhas)
                {
                    var colunas = linha.FindElements(By.CssSelector("td"));
                    var produtoCadastrado = new ProdutoCadastrado
                    {
                        Nome = colunas[0].Text,
                        Fabricante = colunas[1].Text,
                        Preco = Convert.ToDecimal(colunas[2].Text)
                    };
                    produtosCadastrados.Add(produtoCadastrado);
                }
                return produtosCadastrados;
            }
        }
    }
}

