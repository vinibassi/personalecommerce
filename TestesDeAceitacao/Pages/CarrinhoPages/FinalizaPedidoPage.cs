using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Pages.CarrinhoPages
{
    class FinalizaPedidoPage
    {
        public void Close()
        {
            SetupGlobal.Driver.Close();
        }
        public List<ItemPedidoAdicionado> ItensPedidos
        {
            get
            {
                var itensAdicionados = new List<ItemPedidoAdicionado>();
                var linhas = SetupGlobal.Driver.FindElements(By.CssSelector("body > div > table > tbody > tr"));
                foreach (var linha in linhas)
                {
                    var colunas = linha.FindElements(By.CssSelector("td"));
                    var item = new ItemPedidoAdicionado
                    {
                        Produto = colunas[0].Text,
                        Preco = Convert.ToDecimal(colunas[3].Text),
                        Quantidade = Convert.ToInt32(colunas[4].Text)
                    };
                    itensAdicionados.Add(item);
                }
                return itensAdicionados;
            }
        }
    }
}
