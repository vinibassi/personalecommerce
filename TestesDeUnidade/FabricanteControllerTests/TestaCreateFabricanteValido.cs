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
    class TestaCreateFabricanteValido
    {

        private IActionResult result;
        private Mock<IProdutoRepository> mockProdutos;
        private Mock<IFabricanteRepository> mockFabricantes;
        private FabricantesController controller;
        private FabricantesViewModel fabricanteViewModel;
        //private Fabricante fabricante;
        [SetUp]
        public async Task Setup()
        {
            mockFabricantes = new Mock<IFabricanteRepository>();
            mockProdutos = new Mock<IProdutoRepository>();
            controller = new FabricantesController(mockProdutos.Object, mockFabricantes.Object);
            fabricanteViewModel = Generator.ValidFabricanteViewModel();
            // act
            result = await controller.Create(fabricanteViewModel);
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
        public void FabricanteFoiAdicionado()
        {
            mockFabricantes.Verify(f => f.AddFabricanteAsync(It.Is<Fabricante>(fab => fab.Nome == fabricanteViewModel.Nome && 
                                                                                      fab.CNPJ == fabricanteViewModel.CNPJ &&
                                                                                      fab.Endereco == fabricanteViewModel.Endereco)));
        }
    }
}

