using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.ProdutoPages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ProdutoTests
{
    class CriaProdutoTest
    {
        private Produto produto;
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

            p = Generator.ValidProduto();
            p.Fabricante = fabricante;
            context.SaveChanges();

            page = new NewProdutoPage();
            //act
            page.GoToAndLogin();
            page.Visita();
            page.Cadastra(p);
            context = new WebCadastradorContext(builder.Options);
            produto = context.Produto.First();

        }
        [Test]
        public void QuantidadeDeProdutos() => Assert.AreEqual(1, context.Produto.Count());
        [Test]
        public void TestaNome() => Assert.AreEqual(p.Nome, produto.Nome);
        [Test]
        public void TestaPreco() => Assert.AreEqual(p.Preco, produto.Preco);
        [Test]
        public void TestaFabricante() => Assert.AreEqual(p.Fabricante.Nome, produto.Fabricante.Nome);
    }
}
