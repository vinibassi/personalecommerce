using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Testes.ClienteTests
{
    [TestFixture]
    class CriaClienteTest
    {
        private Cliente clienteCadastrado;
        private WebCadastradorContext context;
        private ClientesViewModel novoCliente;

        [OneTimeSetUp]
        public void CadastraCliente()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");
            
            context = new WebCadastradorContext(builder.Options);
            context.Clientes.Clear();
            context.SaveChanges();
            var page = new NewClientesPage();
            novoCliente = Generator.ValidClienteViewModel();
            novoCliente.estadoCivil = EstadoCivil.Casado;
            //act
            page.Navigate();
            page.Cadastra(novoCliente);
            clienteCadastrado = context.Clientes.FirstOrDefault();
        }
        [Test]
        public void QuantidadeDeClientes() => Assert.AreEqual(1, context.Clientes.Count());
        [Test]
        public void TestaNome() => Assert.AreEqual(novoCliente.Nome, clienteCadastrado.Nome);
        [Test]
        public void TestaSobrenome() => Assert.AreEqual(novoCliente.Sobrenome, clienteCadastrado.Sobrenome);
        [Test]
        public void TestaCPF() => Assert.AreEqual(novoCliente.CPF, clienteCadastrado.CPF);
        [Test]
        public void TestaEndereco() => Assert.AreEqual(novoCliente.Endereco, clienteCadastrado.Endereco);
        [Test]
        public void TestaIdade() => Assert.AreEqual(novoCliente.Idade, clienteCadastrado.Idade);
        [Test]
        public void TestaEstadoCivil() => Assert.AreEqual(novoCliente.estadoCivil, clienteCadastrado.EstadoCivil);
    }
}
