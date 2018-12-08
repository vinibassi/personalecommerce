using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using TestesDeAceitacao.Pages.ClientePages;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.ClienteTests
{
    class EditaClienteMesmoCPFTeste
    {
        private DbContextOptionsBuilder<WebCadastradorContext> builder;
        private WebCadastradorContext context;
        private UpdateClientePage page;
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
            cliente1 = new Cliente
            {
                Nome = "Paulo",
                Sobrenome = "Guedes",
                CPF = "00870021087",
                Endereco = "Rua abcdxyz, 23",
                Idade = 20,
                EstadoCivil = EstadoCivil.Solteiro
            };
            context.Clientes.Add(cliente1);
            context.Clientes.Add(new Cliente
            {
                Nome = "Gabriel",
                Sobrenome = "Lopes",
                CPF = "26021494032",
                Endereco = "Rua XYZWABC, 5678",
                Idade = 56,
                EstadoCivil = EstadoCivil.Casado
            });
            context.SaveChanges();
            page = new UpdateClientePage();
            page.NavegaToEdit(cliente1.Id);
            page.ModificaCliente("Pasdd", "SAKDJASKD", "26021494032", "Rua abcdwxyz, 14", 15, EstadoCivil.Divorciado);
        }
        [Test]
        public void CPFNaoMudou()
        {
            context = new WebCadastradorContext(builder.Options);
            var cliente = context.Clientes.First(c => c.Id == cliente1.Id);
            Assert.AreEqual("00870021087", cliente.CPF);
        }
        [Test]
        public void TestaURL() => Assert.AreEqual($"https://localhost:5001/Clientes/Edit/{cliente1.Id}", page.Url);
        [Test]
        public void MensagemDeErroApareceu() => Assert.AreEqual("Este CPF já está cadastrado.", page.LeCPFError());
    }
}
