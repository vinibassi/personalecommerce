using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;
using WebCadastrador.ViewModels;

namespace TestesDeUnidade.FabricanteControllerTests
{
    class TestaEditFabricanteValido
    {
        private FabricantesController controller;
        private IActionResult result;
        private Fabricante fabricante;
        private Mock<IFabricanteRepository> mockFabricantes;
        private Mock<IProdutoRepository> mockProdutos;
        private FabricantesViewModel fabricanteViewModel;

        [SetUp]
        public async Task Setup()
        {
            fabricante = new Fabricante();
            mockProdutos = new Mock<IProdutoRepository>();
            mockFabricantes = new Mock<IFabricanteRepository>();
            mockFabricantes.Setup(f => f.FindByIdAsync(1)).ReturnsAsync(fabricante);
            controller = new FabricantesController(mockProdutos.Object, mockFabricantes.Object);
            // act
            fabricanteViewModel = Generator.ValidFabricanteViewModel();
            mockFabricantes.Setup(f => f.UpdateFabricanteAsync(fabricante));
            result = await controller.Edit(fabricanteViewModel);
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
        public void UpdateFabricanteFoiChamado()
        {
            mockFabricantes.Verify(f => f.UpdateFabricanteAsync(It.Is<Fabricante>(fab => fab.Nome == fabricanteViewModel.Nome &&
                                                                                      fab.CNPJ == fabricanteViewModel.CNPJ &&
                                                                                      fab.Endereco == fabricanteViewModel.Endereco)), Times.Once);
        }
    }
}
