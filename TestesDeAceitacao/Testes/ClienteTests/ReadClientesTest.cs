using Microsoft.EntityFrameworkCore;
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
    class ReadClientesTest
    {
        private Clientes cliente;
        private WebCadastradorContext context;

        [OneTimeSetUp]
        public void CadastraCliente()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Clientes.Clear();
            context.SaveChanges();
            var page = new NewClientesPage();
            page.Navigate();
            //act
            page.Cadastra("Paulo", "Guedes", "00870021087", "Rua abcdwxyz, 14", 15, EstadoCivil.Casado);
            cliente = context.Clientes.FirstOrDefault();
        }
        [Test]
        public void ReadClientes()
        {
            var page = new ClientesListPage();
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Clientes");
            var clienteCadastrado = page.Clientes.FirstOrDefault(c => c.CPF == "008.700.210-87");
            Assert.AreEqual("Guedes", clienteCadastrado.Sobrenome);
        }
    }
}
