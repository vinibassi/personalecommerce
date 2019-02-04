using System;
using OpenQA.Selenium;
using TestesDeUnidade;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Pages.ClientePages
{
    class UpdateClientePage
    {
        public void NavegaToEdit(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Clientes/Edit/{id}");
        }

        public void ModificaCliente(ClientesViewModel novoCliente)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeCliente = driver.FindElement(By.Id("Nome"));
            IWebElement sobrenomeCliente = driver.FindElement(By.Id("Sobrenome"));
            IWebElement cpfCliente = driver.FindElement(By.Id("CPF"));
            IWebElement enderecoCliente = driver.FindElement(By.Id("Endereco"));
            IWebElement idadeCliente = driver.FindElement(By.Id("Idade"));
            switch (novoCliente.estadoCivil)
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

            nomeCliente.SendKeys(novoCliente.Nome);
            sobrenomeCliente.SendKeys(novoCliente.Sobrenome);
            cpfCliente.SendKeys(novoCliente.CPF);
            enderecoCliente.SendKeys(novoCliente.Endereco);
            idadeCliente.SendKeys(novoCliente.Idade.ToString());

            driver.FindElement(By.CssSelector("body > div > div.row > div > form > div:nth-child(8) > input")).Click();
        }
        public void GoToAndLogin()
        {
            var driver = SetupGlobal.Driver;
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            driver.FindElement(By.Id("Input_Email")).SendKeys("vini@vini.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Pipoca@123");
            driver.FindElement(By.Id("login")).Click();
        }
        public string Url=> SetupGlobal.Driver.Url;

        public string LeCPFError()
        {
            var driver = SetupGlobal.Driver;
            var erro = driver.FindElement(By.CssSelector("[data-valmsg-for=CPF]"));
            return erro.Text;
        }
    }
}
