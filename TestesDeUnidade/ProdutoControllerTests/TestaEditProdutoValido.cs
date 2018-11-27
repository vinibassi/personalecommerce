using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using TestesDeUnidade.Mocks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;

namespace TestesDeUnidade.ProdutoController
{
    class TestaEditProdutoValido
    {
        private ProdutoEditViewModel produtoEditVM;
        private IActionResult result;
        private MockFabricanteRepository mockFabricanteRepository;
        private MockProdutoRepositorio mockProdutoRepositorio;
        private ProdutosController controller;

        [SetUp]
        public async Task Setup()
        {
            mockProdutoRepositorio = new MockProdutoRepositorio();
            mockFabricanteRepository = new MockFabricanteRepository();
            controller = new ProdutosController(null, mockProdutoRepositorio, mockFabricanteRepository);
            // act
            produtoEditVM = new ProdutoEditViewModel
            {
                Id = 1,
                Nome = "abc",
                FabricanteId = 1,
                Preco = 49.93m
            };
            result = await controller.Edit(produtoEditVM);
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
        public void UpdateAsyncChamado()
        {
            mockProdutoRepositorio.UpdateAsyncFoiChamado.Should().BeTrue();
        }
        [Test]
        public void ListaFabricantesNãoFoiExibida()
        {
            mockFabricanteRepository.ListaFoiChamada.Should().BeFalse();
        }
    }
}
