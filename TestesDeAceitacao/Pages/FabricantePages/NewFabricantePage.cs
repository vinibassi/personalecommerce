using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages
{
    class NewFabricantePage
    {
        public void Navigate()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Fabricantes/Create");
        }
        public void Cadastra(string nome, string cnpj, string endereco)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeFabricante = driver.FindElement(By.Id("Nome"));
            IWebElement cnpjFabricante = driver.FindElement(By.Id("CNPJ"));
            IWebElement enderecoFabricante = driver.FindElement(By.Id("Endereco"));
            nomeFabricante.SendKeys(nome);
            cnpjFabricante.SendKeys(cnpj);
            enderecoFabricante.SendKeys(endereco);
            driver.FindElement(By.Id("createFabricante")).Click();
        }
        public string LeCNPJError()
        {
            var driver = SetupGlobal.Driver;
            var erro = driver.FindElement(By.CssSelector("[data-valmsg-for=CNPJ]"));
            return erro.Text;
        }
    }
}
