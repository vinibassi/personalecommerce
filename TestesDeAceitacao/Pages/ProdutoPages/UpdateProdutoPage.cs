using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestesDeAceitacao.Pages.ProdutoPages
{
    class UpdateProdutoPage
    {
        public void NavegaToEdit(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Produtos/Edit/{id}");
        }

        public void ModificaProduto(string nome, string fabricante, int preco)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeProduto = driver.FindElement(By.Id("Nome"));
            IWebElement fabricanteProduto = driver.FindElement(By.Id("FabricanteId"));
            IWebElement precoProduto = driver.FindElement(By.Id("Preco"));

            nomeProduto.Clear();
            precoProduto.Clear();
            
            var selectFabricante = new SelectElement(fabricanteProduto);
            
            nomeProduto.SendKeys(nome);
            selectFabricante.SelectByText(fabricante);
            precoProduto.SendKeys(preco.ToString());

            driver.FindElement(By.Id("salvarProduto")).Click();
        }
    }
}
