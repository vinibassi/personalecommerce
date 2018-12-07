using System;
using OpenQA.Selenium;
using WebCadastrador.Models;


namespace TestesDeAceitacao.Pages
{
    class NewClientesPage
    {

        public void Navigate()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Clientes/Create");
        }

        public void Cadastra(string nome, string sobrenome, string cpf, string endereco, int idade, EstadoCivil estadoCivil)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeCliente = driver.FindElement(By.Id("Nome"));
            IWebElement sobrenomeCliente = driver.FindElement(By.Id("Sobrenome"));
            IWebElement cpfCliente = driver.FindElement(By.Id("CPF"));
            IWebElement enderecoCliente = driver.FindElement(By.Id("Endereco"));
            IWebElement idadeCliente = driver.FindElement(By.Id("Idade"));
            switch (estadoCivil)
            {
                case EstadoCivil.Solteiro:
                    driver.FindElement(By.CssSelector("[value=Solteiro]")).Click();
                    break;
                case EstadoCivil.Casado:
                    driver.FindElement(By.CssSelector("[value=Casado]")).Click();
                    break;
                case EstadoCivil.Divorciado:
                    driver.FindElement(By.CssSelector("[value=Divorciado]")).Click();
                    break;
                default:
                    throw new Exception();
            }

            nomeCliente.SendKeys(nome);
            sobrenomeCliente.SendKeys(sobrenome);
            cpfCliente.SendKeys(cpf);
            enderecoCliente.SendKeys(endereco);
            idadeCliente.SendKeys(idade.ToString());
            nomeCliente.Submit();
        }

    }
}
