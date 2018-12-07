using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.FabricantePages;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class DeletaFabricanteTest
    {
        private WebCadastradorContext context;

        [OneTimeSetUp]
        public void DeletaFabricante()
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
            var id = context.Fabricante.First().Id;
            //act
            var page = new DeleteFabricantePage();
            page.NavigateToDeletePage(id);
            page.DeletaFabricante();
        }
        [Test]
        public void TestaQuantidade() => Assert.AreEqual(0, context.Fabricante.Count());
        
    }
}
