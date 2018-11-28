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

namespace TestesDeUnidade.ProdutoController
{
    class TestaCreateProdutoValido
    {
        private ProdutoCreateViewModel produtoCreateVM;
        private IActionResult result;
        private MockFabricanteRepository mockFabricanteRepository;
        private MockProdutoRepositorio mockProdutoRepositorio;
        private ProdutosController controller;

        [SetUp]
        public async Task Setup()
        {
            mockProdutoRepositorio = new MockProdutoRepositorio();
            mockFabricanteRepository = new MockFabricanteRepository();
            controller = new ProdutosController(mockProdutoRepositorio, mockFabricanteRepository);
            // act
            produtoCreateVM = new ProdutoCreateViewModel
            {
                Nome = "abc",
                Fabricante = 1,
                Preco = 49.93m
            };
            result = await controller.Create(produtoCreateVM);
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
            mockProdutoRepositorio.AddAsyncFoiChamado.Should().BeTrue();
        }
        [Test]
        public void ListaFabricantesNãoFoiExibida()
        {
            mockFabricanteRepository.ListaFoiChamada.Should().BeFalse();
        }
    }
}
