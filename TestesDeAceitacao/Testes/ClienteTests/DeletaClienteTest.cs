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
using WebCadastrador.ViewModels;
using WebCadastrador.Models;
using TestesDeAceitacao.Pages.ClientePages;

namespace TestesDeAceitacao.Testes
{
    [TestFixture]
    class DeletaClienteTest
    {
        private Clientes cliente;
        private WebCadastradorContext context;

        [SetUp]
        public void DeletaCliente()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");
            context = new WebCadastradorContext(builder.Options);
            context.Clientes.Clear();
            context.SaveChanges();
            var test = new NewClientesPage();
            test.Navigate();
            //act
            test.Cadastra("Paulo", "Guedes", "00870021087", "Rua abcdwxyz, 14", 15, EstadoCivil.Casado);
            cliente = context.Clientes.FirstOrDefault();
        }
        [Test]
        public void IsNullCliente() => Assert.IsNotNull(cliente);
        [Test]
        public void QuantidadeDeClientes() => Assert.AreEqual(1, context.Clientes.Count());
        [Test]
        public void TestaCPF() => Assert.AreEqual("00870021087", cliente.CPF);
        [Test]
        public void DeleteCliente()
        {
            var page = new DeleteClientPage();
            page.NavigateToDeletePage().DeletaCliente();
        }
    }
}