using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.ProdutoPages;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ProdutoTests
{
    class ReadProdutoTest
    {
        private Produto produto;
        private ProdutoListPage page;
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
                Preco = 13
            });
            context.SaveChanges();
            produto = context.Produto.First();
            page = new ProdutoListPage();
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Produtos");
        }
        [Test]
        public void ListaContemProdutoCerto() => page.Produtos.Single().Should().BeEquivalentTo(new ProdutoCadastrado
        {
            Nome = "Picanha",
            Fabricante = context.Fabricante.First().Nome,
            Preco = 13
        });
        [Test]
        public void ReadProdutos() => Assert.AreEqual("Picanha", produto.Nome);
        [Test]
        public void ListaContemNumeroCertoDeProdutos() => Assert.AreEqual(1, context.Produto.Count());
    }
}
