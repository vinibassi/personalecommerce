using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestesDeAceitacao.Pages.ProdutoPages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ProdutoTests
{
    class CriaProdutoInvalidoTest
    {
        private WebCadastradorContext context;
        private Fabricante fabricante;
        private Produto p;
        private NewProdutoPage page;

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

            fabricante = Generator.ValidFabricante();
            context.Fabricante.Add(fabricante);
            context.SaveChanges();

            p = new Produto
            {
                Fabricante = fabricante,
                FotoUrl = "sadasd",
                Nome = "lalalal",
                Preco = 13.33m
            };
            page = new NewProdutoPage();
            //act
            page.GoToAndLogin();
            page.Visita();
            page.CadastraProdutoInvalido(p);
            context = new WebCadastradorContext(builder.Options);

        }
        [Test]
        public void QuantidadeDeProdutos() => Assert.AreEqual(0, context.Produto.Count());
        [Test]
        public void TestaMensagemDeErro() => Assert.AreEqual("A URL inserida é inválida.", page.LeUrlErro());
        [Test]
        public void TestaURL() => Assert.AreEqual("https://localhost:5001/Produtos/Create", page.Url);

    }
}
