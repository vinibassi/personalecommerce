using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using TestesDeUnidade;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Testes.ClienteTests
{
    class CadastraClienteMesmoCPFTeste
    {
        private ClientesViewModel novoCliente;
        private WebCadastradorContext context;
        private NewClientesPage page;

        [OneTimeSetUp]
        public void CadastraCliente()
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

            page = new NewClientesPage();
            novoCliente = Generator.ValidClienteViewModel();
            novoCliente.CPF = c.CPF;
            //act
            page.Navigate();
            page.Cadastra(novoCliente);
        }
        [Test]
        public void TestaQuantidadeClientes() => Assert.AreEqual(1, context.Clientes.Count());
        [Test]
        public void MensagemDeErroApareceu() => Assert.AreEqual("Este CPF já está cadastrado.", page.LeCPFError());
    }
}
