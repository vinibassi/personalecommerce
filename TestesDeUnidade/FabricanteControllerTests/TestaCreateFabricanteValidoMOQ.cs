using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;
using WebCadastrador.ViewModels;

namespace TestesDeUnidade.FabricanteControllerTests
{
    class TestaCreateFabricanteValidoMOQ
    {

        private IActionResult result;
        private Mock<ProdutoRepository> mockProdutos;
        private Mock<FabricanteRepository> mockFabricantes;
        private FabricantesController controller;
        private FabricantesViewModel fabricanteViewModel;

        [SetUp]
        public async Task Setup()
        {
            mockFabricantes = new Mock<FabricanteRepository>();
            mockProdutos = new Mock<ProdutoRepository>();
            mockFabricantes.Setup(f => f.AddFabricanteAsync(It.IsAny<Fabricante>())).Returns(Task.CompletedTask).Verifiable();

            controller = new FabricantesController(mockProdutos.Object, mockFabricantes.Object);
            fabricanteViewModel = new FabricantesViewModel
            {
                Id = 1,
                Nome = "abc",
                CNPJ = "594780198",
                Endereco = "Rua ABCDXYZ, 123"
            };
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
            mockFabricantes.Verify(f => f.AddFabricanteAsync
                                    (It.Is<Fabricante>
                                    (fab =>fab.Nome == fabricanteViewModel.Nome &&
                                           fab.Endereco == fabricanteViewModel.Endereco &&
                                           fab.CNPJ == fabricanteViewModel.CNPJ &&
                                           fab.Id == fabricanteViewModel.Id)), 
                                     Times.Once());
        }
        
    }
}
