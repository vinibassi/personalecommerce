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
    class FabricanteSystemTest
    {
        private readonly NewFabricantePage page;
        private readonly ChromeDriver driver;

        public FabricanteSystemTest()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            page = new NewFabricantePage(driver);
        }
        [Test]
        public void CadastraFabricante()
        {
            page.Visita();
            page.Cadastra("Bassi LTDA", "94170922000190", "Rua abcdxyz, 23");
            driver.Close();
        }
    }
}
