using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesDeAceitacao;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeIntegracao.TestesDeProduto

{
    class TestaProdutoController
    {
        private Produto produto;
        private ProdutoCreateViewModel produtoCreateVM;
        private Fabricante fabricante;
        private WebCadastradorContext context;
        [OneTimeSetUp]
        public async Task Setup()
        {
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                   .UseLazyLoadingProxies()
                   .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Produto.Clear();
            var controller = new ProdutosController(context, new ProdutoRepository(context), new FabricanteRepository(context));
            // act
            fabricante = new Fabricante
            {
                Nome = "Bassi LTDA",
                CNPJ = "94170922000190",
                Endereco = "Rua abcdxyz, 23"
            };
            context.Add(fabricante);
            context.SaveChanges();
            produtoCreateVM = new ProdutoCreateViewModel
            {
                Nome = "abc",
                Fabricante = fabricante.Id,
                Preco = 49.93m
            };
            context = new WebCadastradorContext(builder.Options);
            var result = await controller.Create(produtoCreateVM);
            produto = context.Produto.FirstOrDefault();
        }
        [Test]
        public void TestaId() => Assert.IsNotNull(produto.Id);
        [Test]
        public void TestaNome() => Assert.AreEqual("abc", produto.Nome);
        [Test]
        public void TestaPreço() => Assert.That(produto.Preco.ToString().EndsWith("3"));
    }
}
