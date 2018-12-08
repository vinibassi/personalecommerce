using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.ProdutoController
{
    class TestaProdutoDetails
    {
        private IActionResult result;
        private Produto produto;
        private Mock<IFabricanteRepository> mockFabricantes;
        private Mock<IProdutoRepository> mockProdutos;
        private ProdutosController controller;

        [SetUp]
        public async Task Setup()
        {
            mockProdutos = new Mock<IProdutoRepository>();
            produto = new Produto
            {
                Id = 1,
                Nome = "abc",
                Preco = 49.93m
            };
            mockFabricantes = new Mock<IFabricanteRepository>();
            mockProdutos.Setup(f => f.FindProdutoByIdAsync(1)).ReturnsAsync(produto);
            controller = new ProdutosController(mockProdutos.Object, mockFabricantes.Object);
            // act
            result = await controller.Details(1);
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            var viewModel = (ProdutoDetailsViewModel)view.Model;
            viewModel.Should().BeEquivalentTo(new ProdutoDetailsViewModel
            {
                Id = 1,
                Nome = "abc",
                Preco = 49.93m
            });
        }
        [Test]
        public void FindFoiChamado() => mockProdutos.Verify(f => f.FindProdutoByIdAsync(1), Times.Once);
    }
}
