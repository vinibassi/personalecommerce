using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.ProdutoPages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;

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

            fabricante = Generator.ValidFabricante();
            context.Fabricante.Add(fabricante);
            context.SaveChanges();

            fabricante = context.Fabricante.First();

            var p = Generator.ValidProduto();
            p.Fabricante = fabricante;
            context.Produto.Add(p);
            context.SaveChanges();
            produto = context.Produto.First();
            var id = context.Produto.First().Id;
            var page = new DeleteProdutoPage();
            //act
            page.GoToAndLogin();
            page.NavigateToDeletePage(id);
            page.DeletaFabricante();
        }
        [Test]
        public void TestaQuantidade() => Assert.AreEqual(0, context.Produto.Count());

    }
}
