using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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

namespace TestesDeUnidade.TestesDeProduto

{
    class TestaProdutoController
    {
        private ProdutoCreateViewModel produtoCreateVM;
        private IActionResult result;
        private Fabricante fabricante;
        private WebCadastradorContext context;
        private ProdutosController controller;

        [SetUp]
        public async Task Setup()
        {
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                   .UseLazyLoadingProxies()
                   .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");
            context = new WebCadastradorContext(builder.Options);
            controller = new ProdutosController(context);
            // act
            produtoCreateVM = new ProdutoCreateViewModel
            {
                Nome = "abc",
                Fabricante = 1,
                Preco = 49.99m
            };
            result = await controller.Create(produtoCreateVM);
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            Assert.AreEqual(produtoCreateVM, view.Model);
        }
        [Test]
        public void TestaModelState()
        {
            controller.ModelState.IsValid.Should().BeFalse();
        }
        [Test]
        public void TestaErroPreco()
        {
            var error = controller.ModelState.Single();
            error.Key.Should().Be("Preco");
            error.Value.Errors.Single().ErrorMessage.Should().Be("O preço deve terminar em 3.");
        }
    }
}
