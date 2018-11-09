using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Pages
{
    class ClientesPage
    {
        readonly IWebDriver driver;

        public ClientesPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Visita()
        {
            driver.Navigate().GoToUrl("https://localhost:44305/Clientes/Create");
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
