using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using TestesDeUnidade.Mocks;
using WebCadastrador.Controllers;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeUnidade.ClienteControllerTests
{
    class TestaEditClienteValido
    {
        private ClientesController controller;
        private IActionResult result;
        private MockClienteRepository mockClienteRepository;

        [SetUp]
        public async Task Setup()
        {
            mockClienteRepository = new MockClienteRepository();
            controller = new ClientesController(mockClienteRepository);
            // act
            var clienteViewModel = new ClientesViewModel
            {
                Id = 1,
                Nome = "abc",
                Sobrenome = "defg",
                CPF = "17997774050",
                Endereco = "Rua ABCDXYZ, 123",
                Idade = 30,
                estadoCivil = EstadoCivil.Divorciado
            };
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
        public void UpdateFabricanteFoiChamado() => mockClienteRepository.UpdateClienteFoiChamado.Should().BeTrue();
    }
}
