using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Support.UI;

namespace TestesDeAceitacao.Pages
{
    class NewProdutoPage
    {
        readonly IWebDriver driver;
        public NewProdutoPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Visita()
        {
            driver.Navigate().GoToUrl("https://localhost:44305/Produtos/Create");
        }

        public void Cadastra(string nome, int preco)
        {
            IWebElement nomeProduto = driver.FindElement(By.Id("Nome"));
            IWebElement precoProduto = driver.FindElement(By.Id("Preco"));
            IWebElement produtoFabricante = driver.FindElement(By.Id("Fabricante"));
            var selectElement = new SelectElement(produtoFabricante);
            selectElement.SelectByValue("1");
            nomeProduto.SendKeys(nome);
            precoProduto.SendKeys(preco.ToString());
            nomeProduto.Submit();
        }
    }
}
