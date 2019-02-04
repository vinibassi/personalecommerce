using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages.ClientePages
{
    class DeleteClientPage
    {
        public void NavigateToDeletePage(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Clientes/Delete/{id}");
        }
        public void GoToAndLogin()
        {
            var driver = SetupGlobal.Driver;
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@admin.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Pass@123");
            driver.FindElement(By.Id("login")).Click();
        }
        public void DeletaCliente()
        {
            SetupGlobal.Driver.FindElement(By.Id("DeletaCliente")).Click();
        }
    }
}
