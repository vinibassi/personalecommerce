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
    class TestaCreateProdutoValido
    {
        private ProdutoCreateViewModel produtoCreateVM;
        private IActionResult result;
        private Mock<IFabricanteRepository> mockFabricantes;
        private Mock<IProdutoRepository> mockProdutos;
        private ProdutosController controller;
        private Fabricante fabricante;
        private Produto produto;

        [SetUp]
        public async Task Setup()
        {
            mockProdutos = new Mock<IProdutoRepository>();
            fabricante = new Fabricante();
            produto = new Produto();
            mockFabricantes = new Mock<IFabricanteRepository>();
            mockFabricantes.Setup(f => f.FindByIdAsync(1)).ReturnsAsync(fabricante);
            mockProdutos.Setup(f => f.AddAsync(produto)).Returns(Task.CompletedTask);
            controller = new ProdutosController(mockProdutos.Object, mockFabricantes.Object);
            produtoCreateVM = Generator.ValidProdutoCreateVM();
            // act
            result = await controller.Create(produtoCreateVM);
        }
        [Test]
        public void TestaResult()
        {
            var view = (RedirectToActionResult)result;
            view.ActionName.Should().Be("Index");
        }
        [Test]
        public void TestaModelState() => controller.ModelState.IsValid.Should().BeTrue();
        [Test]
        public void TestaSemErro() => controller.ModelState.Should().BeEmpty();
        [Test]
        public void AddAsyncChamado()
        {
            mockProdutos.Verify(f=>f.AddAsync(It.Is<Produto>(p => p.Nome == produtoCreateVM.Nome && 
                                                                  p.Fabricante == fabricante && 
                                                                  p.Preco == produtoCreateVM.Preco)), Times.Once);
        }
        [Test]
        public void ListaFabricantesNãoFoiExibida() => Assert.That(controller.ViewBag.Fabricantes, Is.EqualTo(null));
    }
}
