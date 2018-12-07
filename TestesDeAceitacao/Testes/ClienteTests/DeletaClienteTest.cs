using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using WebCadastrador.Models;
using TestesDeAceitacao.Pages.ClientePages;

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
            context.Clientes.Add(new Clientes{
                Nome = "Paulo",
                Sobrenome = "Guedes",
                CPF = "00870021087",
                Endereco = "Rua abcdwxyz, 14",
                Idade = 15,
                EstadoCivil = EstadoCivil.Casado
            });
            context.SaveChanges();
            var id = context.Clientes.First().Id;
            //act
            var page = new DeleteClientPage();
            page.NavigateToDeletePage(id);
            page.DeletaCliente();
        }
        [Test]
        public void QuantidadeDeClientes() => Assert.AreEqual(0, context.Clientes.Count());
    }
}