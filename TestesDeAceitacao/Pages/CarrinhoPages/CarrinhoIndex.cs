using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using WebCadastrador.Models.Carrinho;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Pages.CarrinhoPages
{
    class CarrinhoIndex
    {
        public List<ItemCarrinhoAdicionado> ItensDoCarrinho
        {
            get
            {
                var itensAdicionados = new List<ItemCarrinhoAdicionado>();
                var linhas = SetupGlobal.Driver.FindElements(By.CssSelector("body > div > div.panel.panel-default > div.panel-body"));
                foreach (var linha in linhas)
                {
                    var driver = SetupGlobal.Driver;
                    var produtoNome = driver.FindElement(By.Id("produtoNome")).Text;
                    var produtoPreco = Convert.ToDecimal(driver.FindElement(By.Id("produtoPreco")).GetAttribute("value"));
                    var getProdutoQtd = Convert.ToInt32(driver.FindElement(By.Id("produtoQuantidade")).GetAttribute("value"));
                    var produto = new ItemCarrinhoAdicionado
                    {
                        Produto = produtoNome,
                        Preco = produtoPreco,
                        Quantidade = getProdutoQtd
                    };
                    itensAdicionados.Add(produto);
                }
                return itensAdicionados;
            }
        }
    }
}
