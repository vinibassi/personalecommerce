using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ClienteTests
{
    [TestFixture]
    class CriaClienteTest
    {
        private Clientes novoCliente;
        private WebCadastradorContext context;

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
            page.Navigate();
            page.Cadastra("Paulo", "Guedes", "00870021087", "Rua abcdwxyz, 14", 15, EstadoCivil.Divorciado);
            novoCliente = context.Clientes.FirstOrDefault();
        }
        [Test]
        public void QuantidadeDeClientes() => Assert.AreEqual(1, context.Clientes.Count());
        [Test]
        public void TestaNome() => Assert.AreEqual("Paulo", novoCliente.Nome);
        [Test]
        public void TestaSobrenome() => Assert.AreEqual("Guedes", novoCliente.Sobrenome);
        [Test]
        public void TestaCPF() => Assert.AreEqual("00870021087", novoCliente.CPF);
        [Test]
        public void TestaEndereco() => Assert.AreEqual("Rua abcdwxyz, 14", novoCliente.Endereco);
        [Test]
        public void TestaIdade() => Assert.AreEqual(15, novoCliente.Idade);
        [Test]
        public void TestaEstadoCivil() => Assert.AreEqual(EstadoCivil.Divorciado, novoCliente.EstadoCivil);
    }
}
