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
            c.CPF = "00870021087";
            context.Clientes.Add(c);
            context.SaveChanges();
            var page = new ClientesListPage();
            //act
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Clientes");
            cliente = page.Clientes.FirstOrDefault(c => c.CPF == "008.700.210-87");
        }
        [Test]
        public void ReadClientes() => Assert.AreEqual(c.Sobrenome, cliente.Sobrenome);
    }
}
