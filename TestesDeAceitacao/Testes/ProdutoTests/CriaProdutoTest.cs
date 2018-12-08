using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.ProdutoPages;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ProdutoTests
{
    class CriaProdutoTest
    {
        private Produto produto;
        private WebCadastradorContext context;
        private Fabricante fabricante;
        private NewProdutoPage page;

        [OneTimeSetUp]
        public void CadastraProduto()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");
            page = new NewProdutoPage();
            context = new WebCadastradorContext(builder.Options);
            context.Produto.Clear();
            context.Fabricante.Clear();
            fabricante = new Fabricante
            {
                Nome = "Bassi LTDA",
                CNPJ = "94170922000190",
                Endereco = "Rua abcdxyz, 23"
            };
            context.Fabricante.Add(fabricante);
            context.SaveChanges();
            fabricante = context.Fabricante.First();
            //act
            page.Visita();
            page.Cadastra("Picanha", 13, fabricante.Id);
            produto = context.Produto.First();
        }
        [Test]
        public void QuantidadeDeProdutos() => Assert.AreEqual(1, context.Produto.Count());
        [Test]
        public void TestaNome() => Assert.AreEqual("Picanha", produto.Nome);
        [Test]
        public void TestaPreco() => Assert.AreEqual(13, produto.Preco);
        [Test]
        public void TestaFabricante() => Assert.AreEqual(fabricante.Nome, produto.Fabricante.Nome);
    }
}
