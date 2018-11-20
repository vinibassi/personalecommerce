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
        public DeleteClientPage NavigateToDeletePage()
        {
            SetupGlobal.Driver.FindElement(By.CssSelector("#clientes > tbody > tr > td:nth-child(7) > a:nth-child(3)")).Click();
            return new DeleteClientPage();
        }
        public void DeletaCliente()
        {
            SetupGlobal.Driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();
        }
    }
}
