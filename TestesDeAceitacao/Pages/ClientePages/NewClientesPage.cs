using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using TestesDeUnidade;
using WebCadastrador.Areas.Identity.Data;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Pages
{
    class NewClientesPage
    {
        public void Navigate()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Clientes/Create");
        }
        public void Cadastra(ClientesViewModel novoCliente)
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

            nomeCliente.SendKeys(novoCliente.Nome);
            sobrenomeCliente.SendKeys(novoCliente.Sobrenome);
            cpfCliente.SendKeys(novoCliente.CPF);
            enderecoCliente.SendKeys(novoCliente.Endereco);
            idadeCliente.SendKeys(novoCliente.Idade.ToString());
            nomeCliente.Submit();
        }
        public string LeCPFError()
        {
            var driver = SetupGlobal.Driver;
            var erro = driver.FindElement(By.CssSelector("[data-valmsg-for=CPF]"));
            return erro.Text;
        }
    }
}
