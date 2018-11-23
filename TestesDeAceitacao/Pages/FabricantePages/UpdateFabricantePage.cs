﻿using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Pages.FabricantePages
{
    class UpdateFabricantePage
    {
        public void NavegaToEdit(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:44305/Fabricantes/Edit/{id}");
        }

        public void ModificaFabricante(string nome, string cnpj, string endereco)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeFabricante = driver.FindElement(By.Id("Nome"));
            IWebElement cnpjFabricante = driver.FindElement(By.Id("CNPJ"));
            IWebElement enderecoFabricante = driver.FindElement(By.Id("Endereco"));

            nomeFabricante.Clear();
            cnpjFabricante.Clear();
            enderecoFabricante.Clear();

            nomeFabricante.SendKeys(nome);
            cnpjFabricante.SendKeys(cnpj);
            enderecoFabricante.SendKeys(endereco);

            driver.FindElement(By.CssSelector("body > div > div.row > div > form > div:nth-child(5) > input")).Click();
        }
    }
}