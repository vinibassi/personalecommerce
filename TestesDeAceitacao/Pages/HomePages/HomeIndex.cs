using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestesDeAceitacao.Pages.HomePages
{
    class HomeIndex
    {
        public void Navigate()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/");
        }
        public void AdicionarItemAoCarrinho()
        {
            SetupGlobal.Driver.FindElement(By.CssSelector("#addLink > a")).Click();
        }
    }
}
