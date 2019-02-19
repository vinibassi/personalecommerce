using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages.ProdutoPages
{
    class DeleteProdutoPage
    {
        public void NavigateToDeletePage(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Produtos/Delete/{id}");
        }
        public void DeletaFabricante()
        {
            SetupGlobal.Driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();
        }
    }
}
