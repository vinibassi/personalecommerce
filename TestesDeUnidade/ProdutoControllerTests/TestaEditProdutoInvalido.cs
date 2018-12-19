using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.ProdutoController
{
    public class TestaEditProdutoInvalido
    {
        private ProdutoCreateViewModel produtoCreateVM;
        private ProdutoEditViewModel produtoEditViewModel;
        private IActionResult result;
        private Fabricante fabricante;
        private Mock<IFabricanteRepository> mockFabricantes;
        private Mock<IProdutoRepository> mockProdutos;
        private ProdutosController controller;

        [SetUp]
        public async Task Setup()
        {
            mockProdutos = new Mock<IProdutoRepository>();
            mockFabricantes = new Mock<IFabricanteRepository>();
            controller = new ProdutosController(mockProdutos.Object, mockFabricantes.Object);
            // act
            produtoEditViewModel = Generator.InvalidProdutoEditVM();
            result = await controller.Edit(produtoEditViewModel);
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            Assert.AreEqual(produtoEditViewModel, view.Model);
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
            mockProdutos.Verify(p => p.AddAsync(It.Is<Produto>(prod => prod.Nome == produtoEditViewModel.Nome)), Times.Never);
        }
        [Test]
        public void ListaFabricantesFoiExibida() => Assert.That(controller.ViewBag.Fabricantes, Is.EqualTo(null));
    }
}