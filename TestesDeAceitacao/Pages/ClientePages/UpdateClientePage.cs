using System;
using OpenQA.Selenium;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Pages.ClientePages
{
    class UpdateClientePage
    {
        public void NavegaToEdit(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Clientes/Edit/{id}");
        }

        public void ModificaCliente(string nome, string sobrenome, string cpf, string endereco, int idade, EstadoCivil estadoCivil)
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
            nomeCliente.Clear();
            sobrenomeCliente.Clear();
            cpfCliente.Clear();
            enderecoCliente.Clear();
            idadeCliente.Clear();

            nomeCliente.SendKeys(nome);
            sobrenomeCliente.SendKeys(sobrenome);
            cpfCliente.SendKeys(cpf);
            enderecoCliente.SendKeys(endereco);
            idadeCliente.SendKeys(idade.ToString());

            driver.FindElement(By.CssSelector("body > div > div.row > div > form > div:nth-child(8) > input")).Click();
        }
    }
}
