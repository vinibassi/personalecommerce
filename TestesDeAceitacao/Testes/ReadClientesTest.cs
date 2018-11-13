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

namespace TestesDeAceitacao.Testes
{
    [TestFixture]
    class ReadClientesTest
    {
       
        [Test]
        public void ReadClientes()
        {
            var page = new ClientesListPage();
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:44305/Clientes");
            var clienteCadastrado = page.Clientes.FirstOrDefault(c => c.CPF == "008.700.210-87");
            Assert.AreEqual("Guedes", clienteCadastrado.Sobrenome);
        }
    }
}
