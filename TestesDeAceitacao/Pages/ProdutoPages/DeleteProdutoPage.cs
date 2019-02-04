using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages.ProdutoPages
{
    class DeleteProdutoPage
    {
        public void NavigateToDeletePage(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Produtos/Delete/{id}");
        }
        public void GoToAndLogin()
        {
            var driver = SetupGlobal.Driver;
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            driver.FindElement(By.Id("Input_Email")).SendKeys("vini@vini.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Pipoca@123");
            driver.FindElement(By.Id("login")).Click();
        }
        public void DeletaFabricante()
        {
            SetupGlobal.Driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();
        }
    }
}
