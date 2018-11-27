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
using TestesDeUnidade.Mocks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.ProdutoController
{
    public class TestaCreateProdutoInvalido
    {
        private ProdutoCreateViewModel produtoCreateVM;
        private IActionResult result;
        private Fabricante fabricante;
        private MockFabricanteRepository mockFabricanteRepository;
        private MockProdutoRepositorio mockProdutoRepositorio;
        private ProdutosController controller;

        [SetUp]
        public async Task Setup()
        {
            mockProdutoRepositorio = new MockProdutoRepositorio();
            mockFabricanteRepository = new MockFabricanteRepository();
            controller = new ProdutosController(null, mockProdutoRepositorio, mockFabricanteRepository);
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
        public void TestaModelState() => controller.ModelState.IsValid.Should().BeFalse();
        [Test]
        public void TestaErroPreco()
        {
            var error = controller.ModelState.Single();
            error.Key.Should().Be("Preco");
            error.Value.Errors.Single().ErrorMessage.Should().Be("O preço deve terminar em 3.");
        }
        [Test]
        public void AddAsyncNãoFoiChamado()
        {
            mockProdutoRepositorio.AddAsyncFoiChamado.Should().BeFalse();
        }
        [Test]
        public void ListaFabricantesFoiExibida()
        {
            ((int)controller.ViewBag.Fabricantes.Count).Should().Be(1);
        }
    }
}
