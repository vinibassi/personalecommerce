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
using TestesDeAceitacao.Pages.FabricantePages;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class CriaFabricanteTest
    {
        private Fabricante fabricante;
        private WebCadastradorContext context;

        [OneTimeSetUp]
        public void CadastraFabricante()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Fabricante.Clear();
            context.SaveChanges();
            var page = new NewFabricantePage();
            page.Navigate();
            //act
            page.Cadastra("Bassi LTDA", "94170922000190", "Rua abcdxyz, 23");
            fabricante = context.Fabricante.FirstOrDefault();
        }
        [Test]
        public void IsNullFabricante()
        {
            var fabricanteListPage = new FabricanteListPage();
            var novoFabricante = fabricanteListPage.Fabricante.FirstOrDefault(c => c.Nome == "Bassi LTDA" && c.CNPJ == "94.170.922/0001-90" && c.Endereco == "Rua abcdxyz, 23");
            Assert.IsNotNull(novoFabricante);
        }
        [Test]
        public void QuantidadeDeFabricantes() => Assert.AreEqual(1, context.Fabricante.Count());
        [Test]
        public void TestaNome() => Assert.AreEqual("Bassi LTDA", fabricante.Nome);
        [Test]
        public void TestaCNPJ() => Assert.AreEqual("94170922000190", fabricante.CNPJ);
        [Test]
        public void TestaEndereco() => Assert.AreEqual("Rua abcdxyz, 23", fabricante.Endereco);
    }
}
