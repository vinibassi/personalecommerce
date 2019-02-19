using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Pages.ProdutoPages
{
    class UpdateProdutoPage
    {
        public void NavegaToEdit(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Produtos/Edit/{id}");
        }
        public void ModificaProduto(Produto produtoEditado)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeProduto = driver.FindElement(By.Id("Nome"));
            IWebElement fabricanteProduto = driver.FindElement(By.Id("FabricanteId"));
            IWebElement precoProduto = driver.FindElement(By.Id("Preco"));

            nomeProduto.Clear();
            precoProduto.Clear();
            
            var selectFabricante = new SelectElement(fabricanteProduto);
            
            nomeProduto.SendKeys(produtoEditado.Nome);
            selectFabricante.SelectByText(produtoEditado.Fabricante.Nome);
            precoProduto.SendKeys(produtoEditado.Preco.ToString());

            driver.FindElement(By.Id("salvarProduto")).Click();
        }
    }
}
