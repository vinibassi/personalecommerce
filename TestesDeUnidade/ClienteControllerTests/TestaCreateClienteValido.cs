﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;
using WebCadastrador.ViewModels;

namespace TestesDeUnidade.ClienteControllerTests
{
    class TestaCreateClienteValido
    {
        private ClientesController controller;
        private IActionResult result;
        private Mock<IClienteRepository> mockClientes;
        private ClientesViewModel clienteViewModel;
        [SetUp]
        public async Task Setup()
        {
            mockClientes = new Mock<IClienteRepository>();
            controller = new ClientesController(mockClientes.Object);
            // act
            clienteViewModel = new ClientesViewModel
            {
                Id = 1,
                Nome = "abc",
                Sobrenome = "defg",
                CPF = "17997774050",
                Endereco = "Rua ABCDXYZ, 123",
                Idade = 30,
                estadoCivil = EstadoCivil.Casado
            };
            result = await controller.Create(clienteViewModel);
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
        public void AddAsyncChamado() => mockClientes.Verify(c => c.AddClienteAsync(It.Is<Clientes>(cl => cl.Nome == clienteViewModel.Nome && 
                                                                                                          cl.Sobrenome == clienteViewModel.Sobrenome && 
                                                                                                          cl.CPF == clienteViewModel.CPF && 
                                                                                                          cl.Endereco == clienteViewModel.Endereco &&
                                                                                                          cl.Idade == clienteViewModel.Idade && 
                                                                                                          cl.EstadoCivil == clienteViewModel.estadoCivil)), Times.Once);
    }
}
