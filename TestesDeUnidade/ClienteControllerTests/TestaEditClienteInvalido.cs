using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using TestesDeUnidade.Mocks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;
namespace TestesDeUnidade.ClienteControllerTests
{
    class TestaEditClienteInvalido
    {
        private ClientesController controller;
        private IActionResult result;
        private MockClienteRepository mockClienteRepository;
        private ClientesViewModel clienteViewModel;

        [SetUp]
        public async Task Setup()
        {
            mockClienteRepository = new MockClienteRepository();
            controller = new ClientesController(mockClienteRepository);
            // act
            clienteViewModel = new ClientesViewModel
            {
                Id = 1,
                Nome = "abc",
                Sobrenome = "defg",
                CPF = "774050",
                Endereco = "Rua ABCDXYZ, 123",
                Idade = 30,
                estadoCivil = EstadoCivil.Divorciado
            };
            result = await controller.Edit(clienteViewModel);
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
        public void TestaCPFValido()
        {
            var error = controller.ModelState.Single();
            error.Key.Should().Be("CPF");
            error.Value.Errors.Single().ErrorMessage.Should().Be("O CPF é inválido.");
        }
        [Test]
        public void AddFabricanteNãoFoiChamado() => mockClienteRepository.UpdateClienteFoiChamado.Should().BeFalse();
    }
}
