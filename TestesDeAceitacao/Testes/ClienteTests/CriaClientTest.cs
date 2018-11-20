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
    class CriaClienteTest
    {
        private Clientes novoCliente;
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
            context = new WebCadastradorContext(builder.Options);
            novoCliente = context.Clientes.First();
        }
        [Test]
        public void QuantidadeDeClientes() => Assert.AreEqual(1, context.Clientes.Count());
        [Test]
        public void TestaNome() => Assert.AreEqual("Paulo", novoCliente.Nome);
        [Test]
        public void TestaSobrenome() => Assert.AreEqual("Guedes", novoCliente.Sobrenome);
        [Test]
        public void TestaCPF() => Assert.AreEqual("00870021087", novoCliente.CPF);
        [Test]
        public void TestaEndereco() => Assert.AreEqual("Rua abcdwxyz, 14", novoCliente.Endereco);
        [Test]
        public void TestaIdade() => Assert.AreEqual(15, novoCliente.Idade);
        [Test]
        public void TestaEstadoCivil() => Assert.AreEqual(EstadoCivil.Casado, novoCliente.EstadoCivil);
    }
}
