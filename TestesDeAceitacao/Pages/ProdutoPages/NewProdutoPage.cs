using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestesDeAceitacao.Pages.ProdutoPages
{
    class NewProdutoPage
    {
        
        public void Visita()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Produtos/Create");
        }

        public void Cadastra(string nome, int preco, int fabricanteId)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeProduto = driver.FindElement(By.Id("Nome"));
            IWebElement precoProduto = driver.FindElement(By.Id("Preco"));
            IWebElement produtoFabricante = driver.FindElement(By.Id("Fabricante"));
            var selectElement = new SelectElement(produtoFabricante);
            selectElement.SelectByValue(fabricanteId.ToString());
            nomeProduto.SendKeys(nome);
            precoProduto.SendKeys(preco.ToString());
            nomeProduto.Submit();
        }
    }
}
