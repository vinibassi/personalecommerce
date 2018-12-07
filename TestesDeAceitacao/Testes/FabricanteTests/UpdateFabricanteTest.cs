using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.FabricantePages;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class UpdateFabricanteTest
    {
        private Fabricante fabricante;
        private WebCadastradorContext context;

        [OneTimeSetUp]
        public void UpdateFabricante()
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
            var page = new UpdateFabricantePage();
            var id = context.Fabricante.First().Id;
            page.NavegaToEdit(id);
            page.ModificaFabricante("Bassi LTDA", "94170922000190", "Rua XYZABCD, 32");
            context = new WebCadastradorContext(builder.Options);
            fabricante = context.Fabricante.First();
        }
        [Test]
        public void QuantidadeDeFabricantes() => Assert.AreEqual(1, context.Fabricante.Count());
        [Test]
        public void TestaNewEndereco() => Assert.AreEqual("Rua XYZABCD, 32", fabricante.Endereco);
    }
}
