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

namespace TestesDeAceitacao.Testes.ClienteTests
{
    [TestFixture]
    class UpdateClientTest
    {
        private Clientes cliente;
        private WebCadastradorContext context;

        [OneTimeSetUp]
        public void ModificaCliente()
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
            var page = new UpdateClientePage();
            var id = context.Clientes.First().Id;
            page.NavegaToEdit(id);
            page.ModificaCliente("Paulo", "Guedes", "00870021087", "Rua dos Bobos, no. O", 15, EstadoCivil.Divorciado);
            context = new WebCadastradorContext(builder.Options);
            cliente = context.Clientes.First();
        }
        [Test]
        public void QuantidadeDeClientes() => Assert.AreEqual(1, context.Clientes.Count());
        [Test]
        public void TestaNewEndereco() => Assert.AreEqual("Rua dos Bobos, no. O", cliente.Endereco);
        [Test]
        public void TestaNewEstadoCivil() => Assert.AreEqual(EstadoCivil.Divorciado, cliente.EstadoCivil);

    }
}
