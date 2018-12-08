using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.ProdutoController
{
    class TestaProdutoIndex
    {
        private IActionResult result;
        private Produto produto;
        private Mock<IFabricanteRepository> mockFabricantes;
        private List<Produto> listaP;
        private Mock<IProdutoRepository> mockProdutos;
        private ProdutosController controller;

        [SetUp]
        public async Task Setup()
        {
            mockProdutos = new Mock<IProdutoRepository>();
            produto = new Produto();
            mockFabricantes = new Mock<IFabricanteRepository>();
            listaP = new List<Produto>();
            mockProdutos.Setup(f => f.ListaProdutosAsync()).ReturnsAsync(listaP);
            controller = new ProdutosController(mockProdutos.Object, mockFabricantes.Object);
            // act
            result = await controller.Index();
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            Assert.AreEqual(listaP, view.Model);
        }
        [Test]
        public void TestaIndexList() => mockProdutos.Verify(f => f.ListaProdutosAsync(), Times.Once);
    }
}
