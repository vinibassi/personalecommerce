using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ProdutoTests
{
    class CriaProdutoTest
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
        }
        [Test]
        public void QuantidadeDeProdutos() => Assert.AreEqual(1, context.Produto.Count());
        [Test]
        public void TestaNome() => Assert.AreEqual("Picanha", produto.Nome);
        [Test]
        public void TestaPreco() => Assert.AreEqual(40, produto.Preco);
        [Test]
        public void TestaFabricante() => Assert.AreEqual(fabricante.Nome, produto.Fabricante.Nome);
    }
}
