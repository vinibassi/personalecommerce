using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;
using WebCadastrador.ViewModels;

namespace TestesDeUnidade.FabricanteControllerTests
{
    class TestaCreateFabricanteInvalido
    {
        private FabricantesController controller;
        private IActionResult result;
        private Mock<IProdutoRepository> mockProdutos;
        private Mock<IFabricanteRepository> mockFabricantes;
        private FabricantesViewModel fabricanteViewModel;

        [SetUp]
        public async Task Setup()
        {
            mockProdutos = new Mock<IProdutoRepository>();
            mockFabricantes = new Mock<IFabricanteRepository>();
            controller = new FabricantesController(mockProdutos.Object, mockFabricantes.Object);
            // act
            fabricanteViewModel = Generator.InvalidCNPJFabricanteViewModel();
            result = await controller.Create(fabricanteViewModel);
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            Assert.AreEqual(fabricanteViewModel, view.Model);
        }
        [Test]
        public void TestaModelState() => controller.ModelState.IsValid.Should().BeFalse();
        [Test]
        public void AddFabricanteNaoFoiChamado()
        {
            mockFabricantes.Verify(f=>f.AddFabricanteAsync(It.Is<Fabricante>(fab => fab.Nome == fabricanteViewModel.Nome &&
                                                                                      fab.CNPJ == fabricanteViewModel.CNPJ &&
                                                                                      fab.Endereco == fabricanteViewModel.Endereco)),Times.Never);
        }
        [Test]
        public void TestaCNPJValido()
        {
            var error = controller.ModelState.Single();
            error.Key.Should().Be("CNPJ");
            error.Value.Errors.Single().ErrorMessage.Should().Be("O CNPJ é inválido.");
        }
    }
}
