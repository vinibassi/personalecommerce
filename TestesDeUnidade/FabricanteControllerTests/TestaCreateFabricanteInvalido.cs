using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesDeAceitacao;
using TestesDeUnidade.Mocks;
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
                CNPJ = "594780198",
                Endereco = "Rua ABCDXYZ, 123"
            };
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
        public void AddFabricanteNãoFoiChamado()
        {
            mockFabricanteRepository.AddFabricanteFoiChamado.Should().BeFalse();
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
