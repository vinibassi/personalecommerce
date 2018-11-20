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

namespace TestesDeAceitacao.Testes.ClienteTests
{
    [TestFixture]
    class ReadClientesTest
    {
        private ClienteCadastrado cliente;
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
            context.Clientes.Add(new Clientes
            {
                Nome = "Paulo",
                Sobrenome = "Guedes",
                CPF = "00870021087",
                Endereco = "Rua abcdwxyz, 14",
                Idade = 15,
                EstadoCivil = EstadoCivil.Casado
            });
            context.SaveChanges();
            var page = new ClientesListPage();
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Clientes");
            cliente = page.Clientes.FirstOrDefault(c => c.CPF == "008.700.210-87");
        }
        [Test]
        public void ReadClientes() => Assert.AreEqual("Guedes", cliente.Sobrenome);
    }
}
