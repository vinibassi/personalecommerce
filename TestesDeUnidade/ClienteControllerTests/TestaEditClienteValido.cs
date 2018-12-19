using FluentAssertions;
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
    class TestaEditClienteValido
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
            clienteViewModel = Generator.ValidClienteViewModel();
            result = await controller.Edit(clienteViewModel);
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
        public void UpdateFabricanteFoiChamado() => mockClientes.Verify(c => c.UpdateClienteAsync(It.Is<Cliente>(cl => cl.Nome == clienteViewModel.Nome &&
                                                                                                                        cl.Sobrenome == clienteViewModel.Sobrenome &&
                                                                                                                        cl.CPF == clienteViewModel.CPF &&
                                                                                                                        cl.Endereco == clienteViewModel.Endereco &&
                                                                                                                        cl.Idade == clienteViewModel.Idade &&
                                                                                                                        cl.EstadoCivil == clienteViewModel.estadoCivil)), Times.Once);
    }
}
