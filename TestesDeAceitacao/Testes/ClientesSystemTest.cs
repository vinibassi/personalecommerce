using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TestesDeAceitacao.Pages;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Testes
{
    [TestFixture]
    class ClientesSystemTest
    {
        //private ChromeOptions options;
        //private IWebDriver driver; 
        //[SetUp]
        //public void Setup()
        //{
        //    //options = new ChromeOptions();
        //    //options.AddArgument("--headless");
        //    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)/*, options*/);
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //    driver?.Close();
        //}

        [Test]
        public void CadastraCliente()
        {
            //arrange
            var page = new NewClientesPage();
            page.Navigate();
            //act
            page.Cadastra("Paulo", "Guedes", "00870021087", "Rua abcdwxyz, 14", 15);
            //assert
            var clientListPage = new ClientesListPage();
            var novoCliente = clientListPage.Clientes.FirstOrDefault(c =>c.Nome == "Paulo" && c.Sobrenome == "Guedes" && c.CPF == "008.700.210-87");
            Assert.IsNotNull(novoCliente);
        }
    }
}
