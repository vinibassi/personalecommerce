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
using TestesDeAceitacao.Pages.ProdutoPages;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Testes.ProdutoTests
{
    class DeletaProdutoTest
    {
        private Produto produto;
        private WebCadastradorContext context;
        private Fabricante fabricante;

        [OneTimeSetUp]
        public void CadastraProduto()
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
            fabricante = context.Fabricante.First();
            context.Produto.Add(new Produto
            {
                Nome = "Picanha",
                Fabricante = context.Fabricante.First(),
                Preco = 40
            });
            context.SaveChanges();
            produto = context.Produto.First();
            var id = context.Produto.First().Id;
            //act
            var page = new DeleteProdutoPage();
            page.NavigateToDeletePage(id);
            page.DeletaFabricante();
        }
        [Test]
        public void TestaQuantidade() => Assert.AreEqual(0, context.Produto.Count());

    }
}
