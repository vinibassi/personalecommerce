using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages.FabricantePages
{
    class DeleteFabricantePage
    {
        public void NavigateToDeletePage(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Fabricantes/Delete/{id}");
        }
        public void DeletaFabricante()
        {
            SetupGlobal.Driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();
        }
        public void GoToAndLogin()
        {
            var driver = SetupGlobal.Driver;
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@admin.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Pass@123");
            driver.FindElement(By.Id("login")).Click();
        }

    }
}
