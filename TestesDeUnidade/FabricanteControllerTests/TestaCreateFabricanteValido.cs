using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using TestesDeUnidade.Mocks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeUnidade.FabricanteControllerTests
{
    class TestaCreateFabricanteValido
    {
        private FabricantesController controller;
        private IActionResult result;
        private Fabricante fabricante;
        private MockFabricanteRepository mockFabricanteRepository;
        private MockProdutoRepositorio mockProdutoRepositorio;
        private FabricantesViewModel fabricanteViewModel;

        [SetUp]
        public async Task Setup()
        {
            mockProdutoRepositorio = new MockProdutoRepositorio();
            mockFabricanteRepository = new MockFabricanteRepository();
            controller = new FabricantesController(null, mockProdutoRepositorio, mockFabricanteRepository);
            // act
            fabricanteViewModel = new FabricantesViewModel
            {
                Id = 1,
                Nome = "abc",
                CNPJ = "59478724000198",
                Endereco = "Rua ABCDXYZ, 123"
            };
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
        public void AddAsyncChamado()
        {
            mockFabricanteRepository.AddFabricanteFoiChamado.Should().BeTrue();
        }
    }
}
