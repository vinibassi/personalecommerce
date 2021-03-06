﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;
using WebCadastrador.ViewModels;

namespace TestesDeUnidade.ClienteControllerTests
{
    class TestaCreateClientInvalido
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
            clienteViewModel = Generator.InvalidCPFClienteViewModel();
            result = await controller.Create(clienteViewModel);
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            Assert.AreEqual(clienteViewModel, view.Model);
        }
        [Test]
        public void TestaModelState() => controller.ModelState.IsValid.Should().BeFalse();
        [Test]
        public void AddFabricanteNãoFoiChamado() => mockClientes.Verify(c => c.AddClienteAsync(It.Is<Cliente>(cl => cl.Nome == clienteViewModel.Nome &&
                                                                                                          cl.Sobrenome == clienteViewModel.Sobrenome &&
                                                                                                          cl.CPF == clienteViewModel.CPF &&
                                                                                                          cl.Endereco == clienteViewModel.Endereco &&
                                                                                                          cl.Idade == clienteViewModel.Idade &&
                                                                                                          cl.EstadoCivil == clienteViewModel.estadoCivil)), Times.Never);
        [Test]
        public void TestaCPFValido()
        {
            var error = controller.ModelState.Single();
            error.Key.Should().Be("CPF");
            error.Value.Errors.Single().ErrorMessage.Should().Be("O CPF é inválido.");
        }
    }
}
