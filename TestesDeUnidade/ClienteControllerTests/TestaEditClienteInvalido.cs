using FluentAssertions;
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
    class TestaEditClienteInvalido
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
                CPF = "008700",
                Endereco = "Rua XYZWABC, 123",
                Idade = 35,
                estadoCivil = EstadoCivil.Divorciado
            };
            //act
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
        public void AddFabricanteNãoFoiChamado() => mockClientes.Verify(c => c.UpdateClienteAsync(It.IsAny<Cliente>()), Times.Never);
        //[Test]
        //public void ClienteJaExiste()
        //{
        //    mockClientes.Verify(c => c.ClientesExists(It.Is<Cliente>(cl => 
        //        cl.Id == 1  &&
        //        cl.Nome == "abc" &&
        //        cl.Sobrenome == "defg" &&
        //        cl.CPF == "00870021087" &&
        //        cl.Endereco == "Rua ABCDXYZ, 123" &&
        //        cl.Idade == 30 &&
        //        cl.estadoCivil == EstadoCivil.Divorciado)));
        //}
    }
}
