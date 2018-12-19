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
    class TestaEditProdutoValido
    {
        private ProdutoEditViewModel produtoEditVM;
        private IActionResult result;
        private Mock<IFabricanteRepository> mockFabricantes;
        private Mock<IProdutoRepository> mockProdutos;
        private ProdutosController controller;
        private Fabricante fabricante;
        private Produto produto;

        [SetUp]
        public async Task Setup()
        {
            fabricante = new Fabricante();
            produto = new Produto();
            mockProdutos = new Mock<IProdutoRepository>();
            mockFabricantes = new Mock<IFabricanteRepository>();
            controller = new ProdutosController(mockProdutos.Object, mockFabricantes.Object);
            mockFabricantes.Setup(f => f.FindByIdAsync(1)).ReturnsAsync(fabricante);
            mockProdutos.Setup(p => p.FindProdutoByIdAsync(1)).ReturnsAsync(produto);
            mockProdutos.Setup(p => p.UpdateAsync(produto)).Returns(Task.CompletedTask);
            produtoEditVM = Generator.ValidProdutoEditVM();
            // act
            result = await controller.Edit(produtoEditVM);
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
        public void TestaSemErro()
        {
            controller.ModelState.Should().BeEmpty();
        }
        [Test]
        public void UpdateAsyncChamado()
        {
            mockProdutos.Verify(f=> f.UpdateAsync(It.Is<Produto>(p => p.Nome == produtoEditVM.Nome)), Times.Once);
        }
        [Test]
        public void ListaFabricantesNãoFoiExibida()
        {
            Assert.That(controller.ViewBag.Fabricantes, Is.EqualTo(null));
        }
    }
}
