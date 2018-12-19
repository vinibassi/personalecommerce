using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using WebCadastrador.Models;
using TestesDeAceitacao.Pages.ClientePages;
using TestesDeUnidade;

namespace TestesDeAceitacao.Testes.ClienteTests
{
    [TestFixture]
    class DeletaClienteTest
    {
        private WebCadastradorContext context;

        [SetUp]
        public void DeletaCliente()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");
            context = new WebCadastradorContext(builder.Options);
            context.Clientes.Clear();

            var c = Generator.ValidCliente();
            context.Clientes.Add(c);
            context.SaveChanges();

            var id = context.Clientes.First().Id;
            var page = new DeleteClientPage();
            //act
            page.NavigateToDeletePage(id);
            page.DeletaCliente();
        }
        [Test]
        public void QuantidadeDeClientes() => Assert.AreEqual(0, context.Clientes.Count());
    }
}