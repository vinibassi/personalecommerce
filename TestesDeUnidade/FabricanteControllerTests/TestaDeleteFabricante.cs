using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.FabricanteControllerTests
{
    class TestaDeleteFabricante
    {
        private FabricantesController controller;
        private IActionResult result;
        private Mock<IFabricanteRepository> mockFabricantes;
        private Mock<IProdutoRepository> mockProdutos;
        private Fabricante fabricante;

        [SetUp]
        public async Task Setup()
        {
            fabricante = new Fabricante();
            mockFabricantes = new Mock<IFabricanteRepository>();
            mockFabricantes.Setup(f => f.FindByIdAsync(1)).ReturnsAsync(fabricante);
            mockProdutos = new Mock<IProdutoRepository>();
            controller = new FabricantesController(mockProdutos.Object, mockFabricantes.Object);
            // act
            mockFabricantes.Setup(f => f.AddFabricanteAsync(fabricante));
            result = await controller.DeleteConfirmed(1);
        }
        [Test]
        public void TestaResult()
        {
            var view = (RedirectToActionResult)result;
            view.ActionName.Should().Be("Index");
        }
        [Test]
        public void RemoveFabricanteFoiChamado() => mockFabricantes.Verify(f => f.RemoveFabricanteAsync(fabricante), Times.Once());
    }
}
