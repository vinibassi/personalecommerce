using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;


namespace TestesDeAceitacao.Pages
{
    class NewClientesPage
    {

        public void Navigate()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:44305/Clientes/Create");
        }

        public void Cadastra(string nome, string sobrenome, string cpf, string endereco, int idade)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeCliente = driver.FindElement(By.Id("Nome"));
            IWebElement sobrenomeCliente = driver.FindElement(By.Id("Sobrenome"));
            IWebElement cpfCliente = driver.FindElement(By.Id("CPF"));
            IWebElement enderecoCliente = driver.FindElement(By.Id("Endereco"));
            IWebElement idadeCliente = driver.FindElement(By.Id("Idade"));
            IWebElement estadoCivilCliente = driver.FindElement(By.Name("EstadoCivil"));
            driver.FindElement(By.CssSelector("[value=Divorciado]")).Click();

            nomeCliente.SendKeys(nome);
            sobrenomeCliente.SendKeys(sobrenome);
            cpfCliente.SendKeys(cpf);
            enderecoCliente.SendKeys(endereco);
            idadeCliente.SendKeys(idade.ToString());
            nomeCliente.Submit();
        }

    }
}
