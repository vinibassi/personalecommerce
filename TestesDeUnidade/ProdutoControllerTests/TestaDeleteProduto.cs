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
    class TestaDeleteProduto
    {
        private ProdutoDeleteViewModel produtoDeleteVM;
        private IActionResult result;
        private Fabricante fabricante;
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
            produtoDeleteVM = new ProdutoDeleteViewModel
            {
                Id = 1,
                Nome = "abc",
                Fabricante = 1,
                Preco = 49.99m
            };
            result = await controller.DeleteConfirmed(produtoDeleteVM.Id);
        }
        [Test]
        public void TestaResult()
        {
            var view = (RedirectToActionResult)result;
            view.ActionName.Should().Be("Index");
        }
        [Test]
        public void RemoveAsyncChamado() => mockProdutoRepositorio.RemoveAsyncFoiChamado.Should().BeTrue();
        [Test]
        public void FindProdutoFoiChamado() => mockProdutoRepositorio.FindProdutoFoiChamado.Should().BeTrue();
    }
}
