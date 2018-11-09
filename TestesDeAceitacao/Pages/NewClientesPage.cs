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
        readonly IWebDriver driver;

        public NewClientesPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Visita()
        {
            driver.Navigate().GoToUrl("https://localhost:44305/Clientes/Create");
        }

        public void Cadastra(string nome, string sobrenome, string cpf, string endereco, int idade)
        {
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
            //estadoCivilCliente.SendKeys(estadoCivil.ToString());
            nomeCliente.Submit();
        }

        //public bool Existe(string nome, string sobrenome, string cpf, string endereco, int idade, EstadoCivil estadoCivil)
        //{
        //    bool temNome = driver.PageSource.Contains(nome);
        //    bool temSobrenome = driver.PageSource.Contains(sobrenome);
        //    bool temCpf = driver.PageSource.Contains(cpf);
        //    bool temEndereco = driver.PageSource.Contains(endereco);
        //    bool temIdade = driver.PageSource.Contains(idade.ToString());
        //    bool temEstadoCivil = driver.PageSource.Contains(estadoCivil.ToString());
        //    return temNome && temSobrenome && temCpf && temEndereco && temIdade && temEstadoCivil;
        //}
    }
}
