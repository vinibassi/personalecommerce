using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages.ClientePages
{
    class DeleteClientPage
    {
        public void NavigateToDeletePage(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Clientes/Delete/{id}");
        }
        public void DeletaCliente()
        {
            SetupGlobal.Driver.FindElement(By.Id("DeletaCliente")).Click();
        }
    }
}
