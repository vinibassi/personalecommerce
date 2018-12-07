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
    class TestaEditFabricanteInvalido
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
            controller = new FabricantesController(mockProdutos.Object, mockFabricantes.Object);
            // act
            fabricanteViewModel = new FabricantesViewModel
            {
                Id = 1,
                Nome = "abc",
                CNPJ = "59478a198",
                Endereco = "Rua ABCDXYZ, 123"
            };
            mockFabricantes.Setup(f => f.UpdateFabricanteAsync(fabricante));
            result = await controller.Edit(fabricanteViewModel);
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
        public void TestaCNPJValido()
        {
            var error = controller.ModelState.Single();
            error.Key.Should().Be("CNPJ");
            error.Value.Errors.Single().ErrorMessage.Should().Be("O CNPJ é inválido.");
        }
        [Test]
        public void AddFabricanteNãoFoiChamado()
        {
            mockFabricantes.Verify(f => f.UpdateFabricanteAsync(It.Is<Fabricante>(fab => fab.Nome == fabricanteViewModel.Nome &&
                                                                                      fab.CNPJ == fabricanteViewModel.CNPJ &&
                                                                                      fab.Endereco == fabricanteViewModel.Endereco)), Times.Never);
        }
    }
}
