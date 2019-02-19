using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using TestesDeAceitacao.Pages.ClientePages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Testes.ClienteTests
{
    class EditaClienteCPFJaExiste
    {
        private DbContextOptionsBuilder<WebCadastradorContext> builder;
        private WebCadastradorContext context;
        private UpdateClientePage page;
        private ClientesViewModel novoCliente;
        private Cliente cliente1;

        [OneTimeSetUp]
        public void CadastraCliente()
        {
            //arrange
            builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Clientes.Clear();

            cliente1 = Generator.ValidCliente();
            context.Clientes.Add(cliente1);

            var c2 = Generator.ValidCliente();
            context.Clientes.Add(c2);

            context.SaveChanges();
            page = new UpdateClientePage();
            novoCliente = Generator.ValidClienteViewModel();
            novoCliente.CPF = c2.CPF;
            //act
            page.NavegaToEdit(cliente1.Id);
            page.ModificaCliente(novoCliente);
        }
        [Test]
        public void CPFNaoMudou()
        {
            context = new WebCadastradorContext(builder.Options);
            var cliente = context.Clientes.First(c => c.Id == cliente1.Id);
            Assert.AreEqual(cliente1.CPF, cliente.CPF);
        }
        [Test]
        public void TestaURL() => Assert.AreEqual($"https://localhost:5001/Clientes/Edit/{cliente1.Id}", page.Url);
        [Test]
        public void MensagemDeErroApareceu() => Assert.AreEqual("Este CPF já está cadastrado.", page.LeCPFError());
    }
}
