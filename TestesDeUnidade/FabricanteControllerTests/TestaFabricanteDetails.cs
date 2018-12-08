using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.FabricanteControllerTests
{
    class TestaFabricanteDetails
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
            result = await controller.Details(1);
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            Assert.AreEqual(fabricante, view.Model);
        }
        [Test]
        public void FindFoiChamado() => mockFabricantes.Verify(f => f.FindByIdAsync(1), Times.Once);
    }
}
