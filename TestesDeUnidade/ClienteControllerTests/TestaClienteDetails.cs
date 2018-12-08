using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;
using Moq;
using WebCadastrador.Models.Repositories;

namespace TestesDeUnidade.ClienteControllerTests
{
    class TestaClienteDetails
    {
        private ClientesController controller;
        private IActionResult result;
        private Mock<IClienteRepository> mockClientes;
        private ClientesViewModel clienteViewModel;
        private Clientes cliente;

        [SetUp]
        public async Task Setup()
        {
            mockClientes = new Mock<IClienteRepository>();
            cliente = new Clientes();
            mockClientes.Setup(c => c.FindClienteByIdAsync(1)).ReturnsAsync(cliente);
            controller = new ClientesController(mockClientes.Object);
            // act
            result = await controller.Details(1);
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            Assert.AreEqual(cliente, view.Model);
        }
        [Test]
        public void FindFoiChamado() => mockClientes.Verify(f => f.FindClienteByIdAsync(1), Times.Once);
    }
}
