using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Pages.ProdutoPages
{
    class NewProdutoPage
    {
        
        public void Visita()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Produtos/Create");
        }
        public void GoToAndLogin()
        {
            var driver = SetupGlobal.Driver;
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            driver.FindElement(By.Id("Input_Email")).SendKeys("vini@vini.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Pipoca@123");
            driver.FindElement(By.Id("login")).Click();
        }
        public void Cadastra(Produto novoProduto)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeProduto = driver.FindElement(By.Id("Nome"));
            IWebElement precoProduto = driver.FindElement(By.Id("Preco"));
            IWebElement produtoFabricante = driver.FindElement(By.Id("Fabricante"));
            var selectElement = new SelectElement(produtoFabricante);
            selectElement.SelectByValue(novoProduto.Fabricante.Id.ToString());
            nomeProduto.SendKeys(novoProduto.Nome);
            precoProduto.SendKeys(novoProduto.Preco.ToString());
            nomeProduto.Submit();
        }
    }
}
