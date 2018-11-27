using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using TestesDeUnidade.Mocks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;
using System.Linq;


namespace TestesDeUnidade.ClienteControllerTests
{
    class TestaDeleteCliente
    {
        private ClientesController controller;
        private IActionResult result;
        private MockClienteRepository mockClienteRepository;

        [SetUp]
        public async Task Setup()
        {
            mockClienteRepository = new MockClienteRepository();
            controller = new ClientesController(null, mockClienteRepository);
            // act
            var clienteViewModel = new ClientesViewModel
            {
                Id = 1,
                Nome = "abc",
                Sobrenome = "defg",
                CPF = "17997774050",
                Endereco = "Rua ABCDXYZ, 123",
                Idade = 30,
                estadoCivil = EstadoCivil.Casado
            };
            result = await controller.DeleteConfirmed(clienteViewModel);
        }
        [Test]
        public void TestaResult()
        {
            var view = (RedirectToActionResult)result;
            view.ActionName.Should().Be("Index");
        }
        [Test]
        public void RemoveFabricanteFoiChamado() => mockClienteRepository.RemoveClienteFoiChamado.Should().BeTrue();
        [Test]
        public void FindFabricanteChamado() => mockClienteRepository.FindClienteFoiChamado.Should().BeTrue();
    }
}
