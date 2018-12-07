using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using WebCadastrador.Models;
using WebCadastrador.Controllers;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class CadastraFabricanteMesmoCNPJTeste
    {
        private WebCadastradorContext context;
        private NewFabricantePage page;
        private FabricantesController controller;

        [OneTimeSetUp]
        public void CadastraFabricante()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Produto.Clear();
            context.Fabricante.Clear();
            context.Fabricante.Add(new Fabricante
            {
                Nome = "Bassi LTDA",
                CNPJ = "94170922000190",
                Endereco = "Rua abcdxyz, 23"
            });
            context.SaveChanges();
            page = new NewFabricantePage();
            page.Navigate();
            page.Cadastra("Jajjajada", "94170922000190", "rua wxyz, 32");
        }
        [Test]
        public void QuantidadeDeFabricantes() => Assert.AreEqual(1, context.Fabricante.Count());
        [Test]
        public void MensagemDeErroApareceu() => Assert.AreEqual("Este CNPJ já está cadastrado.", page.LeCNPJError());
    }
}
