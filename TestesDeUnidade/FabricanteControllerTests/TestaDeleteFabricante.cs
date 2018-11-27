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
    class TestaDeleteFabricante
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
            result = await controller.DeleteConfirmed(fabricanteViewModel.Id);
        }
        [Test]
        public void TestaResult()
        {
            var view = (RedirectToActionResult)result;
            view.ActionName.Should().Be("Index");
        }
        [Test]
        public void RemoveFabricanteFoiChamado() => mockFabricanteRepository.RemoveFabricanteFoiChamado.Should().BeTrue();
        [Test]
        public void FindFabricanteChamado() => mockFabricanteRepository.FindFabricanteFoiChamado.Should().BeTrue();
    }
}
