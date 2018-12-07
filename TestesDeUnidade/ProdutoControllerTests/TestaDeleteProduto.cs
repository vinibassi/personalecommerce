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
    class TestaDeleteProduto
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
            produto = new Produto();
            mockFabricantes = new Mock<IFabricanteRepository>();
            mockProdutos.Setup(f => f.FindProdutoByIdAsync(1)).ReturnsAsync(produto);
            controller = new ProdutosController(mockProdutos.Object, mockFabricantes.Object);
            // act
            mockProdutos.Setup(f => f.RemoveAsync(produto)).Returns(Task.CompletedTask);
            result = await controller.DeleteConfirmed(1);
        }
        [Test]
        public void TestaResult()
        {
            var view = (RedirectToActionResult)result;
            view.ActionName.Should().Be("Index");
        }
        [Test]
        public void RemoveAsyncChamado() => mockProdutos.Verify(f => f.RemoveAsync(produto), Times.Once);
    }
}
