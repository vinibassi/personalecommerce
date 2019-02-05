using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Data;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeIntegracao.TestesDeProduto
{
    class TestaCreateProdutoPrecoErrado
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
            // act
            fabricante = new Fabricante
            {
                Nome = "Bassi LTDA",
                CNPJ = "94170922000190",
                Endereco = "Rua abcdxyz, 23"
            };
            context.Add(fabricante);
            context.SaveChanges();
            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                {"Nome","abc"},
                {"Fabricante",fabricante.Id.ToString()},
                {"Preco","9.99"}
            });
            response = await SetupGlobal.HttpClient.PostAsync("http://localhost/Produtos/Create", content);
            //assert
            context = new WebCadastradorContext(builder.Options);
            produto = context.Produto.FirstOrDefault();
        }
        [Test]
        public void TestaResponse() => response.StatusCode.Should().Be(HttpStatusCode.OK);

        [Test]
        public void UrlEhAMesma() => response.RequestMessage.RequestUri.ToString().Should().Be("http://localhost/Produtos/Create");

        [Test]
        public async Task ReadErro()
        {
            var content = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            doc.DocumentNode.SelectNodes("//*[@data-valmsg-for='Preco']").Single().InnerText.Should().Be("O pre&#xE7;o deve terminar em 3.");

        }

    }
}
