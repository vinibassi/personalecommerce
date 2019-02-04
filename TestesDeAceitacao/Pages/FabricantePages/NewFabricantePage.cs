using OpenQA.Selenium;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Pages
{
    class NewFabricantePage
    {
        public void Navigate()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Fabricantes/Create");
        }
        public void Cadastra(FabricantesViewModel novoFab)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeFabricante = driver.FindElement(By.Id("Nome"));
            IWebElement cnpjFabricante = driver.FindElement(By.Id("CNPJ"));
            IWebElement enderecoFabricante = driver.FindElement(By.Id("Endereco"));
            nomeFabricante.SendKeys(novoFab.Nome);
            cnpjFabricante.SendKeys(novoFab.CNPJ);
            enderecoFabricante.SendKeys(novoFab.Endereco);
            driver.FindElement(By.Id("createFabricante")).Click();
        }
        public string LeCNPJError()
        {
            var driver = SetupGlobal.Driver;
            var erro = driver.FindElement(By.CssSelector("[data-valmsg-for=CNPJ]"));
            return erro.Text;
        }
        public void GoToAndLogin()
        {
            var driver = SetupGlobal.Driver;
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            driver.FindElement(By.Id("Input_Email")).SendKeys("vini@vini.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Pipoca@123");
            driver.FindElement(By.Id("login")).Click();
        }
    }

}
