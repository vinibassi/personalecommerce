using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestesDeAceitacao.Pages
{
    class NewFabricantePage
    {
        readonly IWebDriver driver;
        public NewFabricantePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Visita()
        {
            driver.Navigate().GoToUrl("https://localhost:44305/Fabricantes/Create");
        }

        public void Cadastra(string nome, string cnpj, string endereco)
        {
            IWebElement nomeFabricante = driver.FindElement(By.Id("Nome"));
            IWebElement cnpjFabricante = driver.FindElement(By.Id("CNPJ"));
            IWebElement enderecoFabricante = driver.FindElement(By.Id("Endereco"));
            //var selectElement = new SelectElement(enderecoFabricante);
            //selectElement.SelectByValue("1");
            nomeFabricante.SendKeys(nome);
            cnpjFabricante.SendKeys(cnpj);
            enderecoFabricante.SendKeys(endereco);
            nomeFabricante.Submit();
        }
    }
}
