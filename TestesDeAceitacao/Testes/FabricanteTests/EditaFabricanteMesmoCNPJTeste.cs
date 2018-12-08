using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using WebCadastrador.Models;
using WebCadastrador.Controllers;
using TestesDeAceitacao.Pages.FabricantePages;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class EditaFabricanteMesmoCNPJTeste
    {
        private DbContextOptionsBuilder<WebCadastradorContext> builder;
        private WebCadastradorContext context;
        private UpdateFabricantePage page;
        private Fabricante fabricante1;

        [OneTimeSetUp]
        public void CadastraFabricante()
        {
            //arrange
            builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Produto.Clear();
            context.Fabricante.Clear();
            fabricante1 = new Fabricante
            {
                Nome = "Bassi LTDA",
                CNPJ = "94170922000190",
                Endereco = "Rua abcdxyz, 23"
            };
            context.Fabricante.Add(fabricante1);
            context.Fabricante.Add(new Fabricante
            {
                Nome = "Bluv  LTDA",
                CNPJ = "18270411000162",
                Endereco = "Rua XYZAWABCS, 2232"
            });
            context.SaveChanges();
            page = new UpdateFabricantePage();
            page.NavegaToEdit(fabricante1.Id);
            page.ModificaFabricante("Jajjajada", "18270411000162", "rua wxyz, 32");
        }
        [Test]
        public void CNPJNaoMudou()
        {
            context = new WebCadastradorContext(builder.Options);
            var cliente = context.Fabricante.First(c => c.Id == fabricante1.Id);
            Assert.AreEqual("94170922000190", fabricante1.CNPJ);
        }
        [Test]
        public void TestaURL() => Assert.AreEqual($"https://localhost:5001/Fabricantes/Edit/{fabricante1.Id}", page.Url);
        [Test]
        public void MensagemDeErroApareceu() => Assert.AreEqual("Este CNPJ já está cadastrado.", page.LeCnpjError());
    }
}
