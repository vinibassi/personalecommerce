using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ClienteTests
{
    class CadastraClienteMesmoCPFTeste
    {
        private Cliente novoCliente;
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
            context.Clientes.Add(new Cliente
            {
                Nome = "Paulo",
                Sobrenome = "Guedes",
                CPF = "00870021087",
                Endereco = "Rua abcdxyz, 23",
                Idade = 20,
                EstadoCivil = EstadoCivil.Casado
            });
            context.SaveChanges();
            page = new NewClientesPage();
            page.Navigate();
            page.Cadastra("dasdsadsadsa", "SAKDJASKD", "00870021087", "Rua abcdwxyz, 14", 15, EstadoCivil.Divorciado);
        }
        [Test]
        public void TestaQuantidadeClientes() => Assert.AreEqual(1, context.Clientes.Count());
        [Test]
        public void MensagemDeErroApareceu() => Assert.AreEqual("Este CPF já está cadastrado.", page.LeCPFError());
    }
}
