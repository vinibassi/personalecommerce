using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.ProdutoPages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ProdutoTests
{
    class ReadProdutoTest
    {
        private ProdutoListPage page;
        private WebCadastradorContext context;
        private Fabricante fabricante;
        private Produto p;

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

            p = Generator.ValidProduto();
            p.Fabricante = fabricante;
            context.Produto.Add(p);
            context.SaveChanges();

            page = new ProdutoListPage();
            //act
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Produtos");
        }
        [Test]
        public void ListaContemProdutoCerto() => page.Produtos.Single().Should().BeEquivalentTo(new ProdutoCadastrado
        {
            Nome = p.Nome,
            Fabricante = fabricante.Nome,
            Preco = p.Preco
        });
        [Test]
        public void ListaContemNumeroCertoDeProdutos() => Assert.AreEqual(1, context.Produto.Count());
    }
}
