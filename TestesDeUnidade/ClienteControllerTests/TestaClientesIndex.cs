using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;
using Moq;
using WebCadastrador.Models.Repositories;
using System.Collections.Generic;

namespace TestesDeUnidade.ClienteControllerTests
{
    class TestaClientesIndex
    {
        private ClientesController controller;
        private IActionResult result;
        private Mock<IClienteRepository> mockClientes;
        private Cliente cliente;
        private List<Cliente> listaC;

        [SetUp]
        public async Task Setup()
        {
            mockClientes = new Mock<IClienteRepository>();
            cliente = new Cliente();
            listaC = new List<Cliente>();
            mockClientes.Setup(c => c.ListaClientesAsync()).ReturnsAsync(listaC);
            controller = new ClientesController(mockClientes.Object);
            // act
            result = await controller.Index();
        }
        [Test]
        public void TestaResult()
        {
            var view = (ViewResult)result;
            Assert.AreEqual(listaC, view.Model);
        }
        [Test]
        public void TestaIndexList() => mockClientes.Verify(f => f.ListaClientesAsync(), Times.Once);
    }
}
