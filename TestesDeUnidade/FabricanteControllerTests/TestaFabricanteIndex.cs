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
    class TestaFabricanteIndex
    {
        private FabricantesController controller;
        private IActionResult result;
        private Mock<IFabricanteRepository> mockFabricantes;
        private List<Fabricante> listaF;
        private Mock<IProdutoRepository> mockProdutos;
        private Fabricante fabricante;

        [SetUp]
        public async Task Setup()
        {
            fabricante = new Fabricante();
            mockFabricantes = new Mock<IFabricanteRepository>();
            listaF = new List<Fabricante>();
            mockFabricantes.Setup(f => f.ListaFabricantesAsync()).ReturnsAsync(listaF);
            mockProdutos = new Mock<IProdutoRepository>();
            controller = new FabricantesController(mockProdutos.Object, mockFabricantes.Object);
            // act
            result = await controller.Index();
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            Assert.AreEqual(listaF, view.Model);
        }
        [Test]
        public void TestaIndexList() => mockFabricantes.Verify(f => f.ListaFabricantesAsync(), Times.Once);
    }
}
