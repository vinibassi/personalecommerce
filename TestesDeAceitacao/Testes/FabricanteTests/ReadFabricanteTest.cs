﻿using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using WebCadastrador.Models;
using TestesDeAceitacao.Pages.FabricantePages;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class ReadFabricanteTest
    {
        private FabricanteCadastrado fabricante;
        private WebCadastradorContext context;

        [OneTimeSetUp]
        public void ModificaCliente()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Produto.Clear();
            context.Fabricante.Clear();
            context.Fabricante.Add(new Fabricante
            {
                Nome = "Bassi LTDA",
                CNPJ = "94170922000190",
                Endereco = "Rua abcdxyz, 23"
            });
            context.SaveChanges();
            var page = new FabricanteListPage();
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Fabricantes");
            fabricante = page.Fabricante.FirstOrDefault(c => c.CNPJ == "94.170.922/0001-90");
        }
        [Test]
        public void ReadFabricantes() => Assert.AreEqual("Bassi LTDA", fabricante.Nome);
    }
}