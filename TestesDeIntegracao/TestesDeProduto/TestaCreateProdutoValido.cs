using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestesDeUnidade;
using WebCadastrador.Controllers;
using WebCadastrador.Data;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeIntegracao.TestesDeProduto

{
    class TestaCreateProdutoValido
    {
        private Produto produto;
        private ProdutoCreateViewModel produtoCreateVM;
        private Fabricante fabricante;
        private HttpResponseMessage response;
        private WebCadastradorContext context;
        [OneTimeSetUp]
        public async Task Setup()
        {
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                   .UseLazyLoadingProxies()
                   .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Produto.Clear();
            var controller = new ProdutosController(new ProdutoRepository(context), new FabricanteRepository(context));

            fabricante = Generator.ValidFabricante();
            context.Add(fabricante);
            context.SaveChanges();
            
            // act
            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                {"Nome","abc"},
                {"Fabricante",fabricante.Id.ToString()},
                {"Preco","9.93"}
            });
            response = await SetupGlobal.HttpClient.PostAsync("http://localhost/Produtos/Create", content);
            //assert
            context = new WebCadastradorContext(builder.Options);
            produto = context.Produto.FirstOrDefault();
        }

        [Test]
        public void TestaResponseOK() => response.StatusCode.Should().Be(HttpStatusCode.OK);
        [Test]
        public void FoiRedirecionadoParaHomeDeProdutos() => response.RequestMessage.RequestUri.ToString().Should().Be("http://localhost/Produtos");
        [Test]
        public void TestaId() => Assert.IsNotNull(produto.Id);
        [Test]
        public void TestaNome() => Assert.AreEqual("abc", produto.Nome);
        [Test]
        public void TestaPreço() => Assert.That(produto.Preco.ToString().EndsWith("3"));
    }
}
