using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ClienteTests
{
    [TestFixture]
    class ReadClientesTest
    {
        private ClientesListPage page;
        private ClienteCadastrado cliente;
        private WebCadastradorContext context;
        private Cliente c;

        [OneTimeSetUp]
        public void CadastraCliente()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");
            context = new WebCadastradorContext(builder.Options);
            context.Clientes.Clear();

            c = Generator.ValidCliente();
            context.Clientes.Add(c);
            context.SaveChanges();

            page = new ClientesListPage();
            //act
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Clientes");
        }
        [Test]
        public void ListaContemClienteCerto() => page.Clientes.Single().Should().BeEquivalentTo(new ClienteCadastrado
        {
            Nome = c.Nome,
            CPF = Formatador.CPF(c.CPF) ,
            Sobrenome = c.Sobrenome
        });
        [Test]
        public void ListaContemNumeroCertoDeClientes() => Assert.AreEqual(1, context.Clientes.Count());
    }
}
