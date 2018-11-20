using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Pages.ClientePages
{
    class UpdateClientePage
    {
        public void ClicaEmEdit()
        {
            SetupGlobal.Driver.FindElement(By.CssSelector("#clientes > tbody > tr > td:nth-child(7) > a:nth-child(1)")).Click();
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
