using System;
using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages.FabricantePages
{
    class UpdateFabricantePage
    {
        public string Url => SetupGlobal.Driver.Url;

        public void NavegaToEdit(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Fabricantes/Edit/{id}");
        }

        public void ModificaFabricante(string nome, string cnpj, string endereco)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeFabricante = driver.FindElement(By.Id("Nome"));
            IWebElement cnpjFabricante = driver.FindElement(By.Id("CNPJ"));
            IWebElement enderecoFabricante = driver.FindElement(By.Id("Endereco"));

            nomeFabricante.Clear();
            cnpjFabricante.Clear();
            enderecoFabricante.Clear();

            nomeFabricante.SendKeys(nome);
            cnpjFabricante.SendKeys(cnpj);
            enderecoFabricante.SendKeys(endereco);

            driver.FindElement(By.CssSelector("body > div > div.row > div > form > div:nth-child(5) > input")).Click();
        }

        public string LeCnpjError()
        {
            var driver = SetupGlobal.Driver;
            var erro = driver.FindElement(By.CssSelector("[data-valmsg-for=CNPJ]"));
            return erro.Text;
        }
    }
}
