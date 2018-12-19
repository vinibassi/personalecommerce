using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private Mock<IFabricanteRepository> mockFabricantes;
        private Mock<IProdutoRepository> mockProdutos;
        private ProdutosController controller;

        [SetUp]
        public async Task Setup()
        {
            mockProdutos = new Mock<IProdutoRepository>();
            mockFabricantes = new Mock<IFabricanteRepository>();
            mockFabricantes.Setup(f => f.ListaFabricantesAsync()).ReturnsAsync(new List<Fabricante> { new Fabricante { Id = 999 } }).Verifiable();
            controller = new ProdutosController(mockProdutos.Object, mockFabricantes.Object);
            // act
            produtoCreateVM = Generator.InvalidProdutoCreateVM();
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
            mockProdutos.Verify(f => f.AddAsync(It.Is<Produto>(p => p.Nome == produtoCreateVM.Nome && 
                                                                    p.Fabricante == fabricante &&
                                                                    p.Preco == produtoCreateVM.Preco)), Times.Never);
        }
        [Test]
        public void FabricantesForamObtidos() => mockFabricantes.VerifyAll();

        [Test]
        public void ListaFabricantesFoiExibida()
        {
            Assert.That(controller.ViewBag.Fabricantes, Has.Count.EqualTo(1));
            Assert.That(controller.ViewBag.Fabricantes[0], Has.Property("Value").EqualTo("999"));
        }
    }
}
