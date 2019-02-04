using System;
using OpenQA.Selenium;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Pages.FabricantePages
{
    class UpdateFabricantePage
    {
        public string Url => SetupGlobal.Driver.Url;

        public void NavegaToEdit(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Fabricantes/Edit/{id}");
        }

        public void ModificaFabricante(FabricantesViewModel fabricanteEditado)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeFabricante = driver.FindElement(By.Id("Nome"));
            IWebElement cnpjFabricante = driver.FindElement(By.Id("CNPJ"));
            IWebElement enderecoFabricante = driver.FindElement(By.Id("Endereco"));

            nomeFabricante.Clear();
            cnpjFabricante.Clear();
            enderecoFabricante.Clear();

            nomeFabricante.SendKeys(fabricanteEditado.Nome);
            cnpjFabricante.SendKeys(fabricanteEditado.CNPJ);
            enderecoFabricante.SendKeys(fabricanteEditado.Endereco);

            driver.FindElement(By.CssSelector("body > div > div.row > div > form > div:nth-child(5) > input")).Click();
        }
        public void GoToAndLogin()
        {
            var driver = SetupGlobal.Driver;
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@admin.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Pass@123");
            driver.FindElement(By.Id("login")).Click();
        }
        public string LeCnpjError()
        {
            var driver = SetupGlobal.Driver;
            var erro = driver.FindElement(By.CssSelector("[data-valmsg-for=CNPJ]"));
            return erro.Text;
        }
    }
}
