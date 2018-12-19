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
    class TestaDeleteCliente
    {
        private ClientesController controller;
        private IActionResult result;
        private Mock<IClienteRepository> mockClientes;
        private ClientesViewModel clienteViewModel;
        private Cliente cliente;

        [SetUp]
        public async Task Setup()
        {
            mockClientes = new Mock<IClienteRepository>();
            cliente = new Cliente();
            mockClientes.Setup(c => c.FindClienteByIdAsync(1)).ReturnsAsync(cliente);
            controller = new ClientesController(mockClientes.Object);
            // act
            clienteViewModel = Generator.ValidClienteViewModel();
            result = await controller.DeleteConfirmed(clienteViewModel);
        }
        [Test]
        public void TestaResult()
        {
            var view = (RedirectToActionResult)result;
            view.ActionName.Should().Be("Index");
        }
        [Test]
        public void RemoveClienteFoiChamado() => mockClientes.Verify(c => c.RemoveClienteAsync(cliente), Times.Once);
        [Test]
        public void FindClienteChamado() => mockClientes.Verify(c => c.FindClienteByIdAsync(1), Times.Once);
    }
}
