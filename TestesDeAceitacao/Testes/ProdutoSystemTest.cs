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
    class ProdutoSystemTest
    {
        private readonly NewProdutoPage page;
        private readonly ChromeDriver driver;

        public ProdutoSystemTest()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            page = new NewProdutoPage(driver);
        }
        [Test]
        public void CadastraProduto()
        {
            page.Visita();
            page.Cadastra("Ração", 20);
            driver.Close();
        }



    }
}
