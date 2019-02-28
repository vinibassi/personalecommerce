using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestesDeAceitacao.Pages.HomePages
{
    class HomeIndex
    {
        public void DeletaCookies()
        {
            SetupGlobal.Driver.Manage().Cookies.DeleteAllCookies();
            SetupGlobal.GoToAndLogin();
        }

        public void Navigate()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/");
        }
        public void AdicionarItemAoCarrinho()
        {
            SetupGlobal.Driver.FindElement(By.Id("addItem")).Click();
        }
    }
}
