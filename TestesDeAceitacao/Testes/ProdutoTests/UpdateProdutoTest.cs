using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.ProdutoPages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ProdutoTests
{
    class UpdateProdutoTest
    {
        private Produto produto;
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

            p = Generator.ValidProduto();
            context.Produto.Add(p);
            p.Fabricante = fabricante;
            context.SaveChanges();

            produto = context.Produto.First();

            context = new WebCadastradorContext(builder.Options);
            var page = new UpdateProdutoPage();
            var id = context.Produto.First().Id;
            //act
            page.GoToAndLogin();
            page.NavegaToEdit(id);
            page.ModificaProduto(p);
            produto = context.Produto.First();
        }
        [Test]
        public void QuantidadeDeProdutos() => Assert.AreEqual(1, context.Produto.Count());
        [Test]
        public void TestaNewPreco() => Assert.AreEqual(p.Preco, produto.Preco);
        [Test]
        public void TestaNewNome() => Assert.AreEqual(p.Nome, produto.Nome);
        [Test]
        public void TestaNewId() => Assert.AreEqual(p.Id, produto.Id);
    }
}
