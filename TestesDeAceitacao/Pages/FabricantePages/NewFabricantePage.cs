﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestesDeAceitacao.Pages
{
    class NewFabricantePage
    {
        public void Navigate()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Fabricantes/Create");
        }
        public void Cadastra(string nome, string cnpj, string endereco)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeFabricante = driver.FindElement(By.Id("Nome"));
            IWebElement cnpjFabricante = driver.FindElement(By.Id("CNPJ"));
            IWebElement enderecoFabricante = driver.FindElement(By.Id("Endereco"));
            nomeFabricante.SendKeys(nome);
            cnpjFabricante.SendKeys(cnpj);
            enderecoFabricante.SendKeys(endereco);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[4]/input")).Click();
        }
    }
}