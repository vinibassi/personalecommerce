using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using WebCadastrador.Models;
using TestesDeAceitacao.Pages.ClientePages;
using TestesDeUnidade;
using WebCadastrador.ViewModels;
using WebCadastrador.Data;

namespace TestesDeAceitacao.Testes.ClienteTests
{
    [TestFixture]
    class UpdateClientTest
    {
        private Cliente cliente;
        private WebCadastradorContext context;
        private ClientesViewModel clienteEditado;

        [OneTimeSetUp]
        public void ModificaCliente()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");
            context = new WebCadastradorContext(builder.Options);
            context.Clientes.Clear();

            var c = Generator.ValidCliente();
            c.CPF = "00870021087";
            context.Clientes.Add(c);

            context.SaveChanges();

            var page = new UpdateClientePage();
            var id = context.Clientes.First().Id;
            clienteEditado = Generator.ValidClienteViewModel();
            clienteEditado.estadoCivil = EstadoCivil.Casado;
            //act
            page.GoToAndLogin();
            page.NavegaToEdit(id);
            page.ModificaCliente(clienteEditado);

            context = new WebCadastradorContext(builder.Options);
            cliente = context.Clientes.First();
            clienteEditado.CPF = cliente.CPF;
        }
        [Test]
        public void QuantidadeDeClientes() => Assert.AreEqual(1, context.Clientes.Count());
        [Test]
        public void TestaNewEndereco() => Assert.AreEqual(clienteEditado.Endereco, cliente.Endereco);
        [Test]
        public void TestaNewEstadoCivil() => Assert.AreEqual(clienteEditado.estadoCivil, cliente.EstadoCivil);
        [Test]
        public void TestaNewNome() => Assert.AreEqual(clienteEditado.Nome, cliente.Nome);
        [Test]
        public void TestaNewSobrenome() => Assert.AreEqual(clienteEditado.Sobrenome, cliente.Sobrenome);
        [Test]
        public void TestaNewIdade() => Assert.AreEqual(clienteEditado.Idade, cliente.Idade);
    }
}
