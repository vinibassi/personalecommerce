using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
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
        private ChromeOptions options;
        private NewClientesPage page;
        private IWebDriver driver; 
        public ClientesSystemTest()
        {
            //options = new ChromeOptions();
            //options.AddArgument("--headless");
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)/*, options*/);
            page = new NewClientesPage(driver);
        }

        [Test]
        public void CadastraCliente()
        {
            page.Visita();
            page.Cadastra("Paulo", "Guedes", "00870021087", "Rua abcdwxyz, 14", 15);
            driver.Close();
        }
    }
}
