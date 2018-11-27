using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

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
