using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.FabricantePages;
using TestesDeUnidade;
using WebCadastrador.Data;

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

            var f = Generator.ValidFabricante();
            context.Fabricante.Add(f);
            context.SaveChanges();
            var page = new DeleteFabricantePage();
            var id = context.Fabricante.First().Id;
            
            //act
            page.NavigateToDeletePage(id);
            page.DeletaFabricante();
        }
        [Test]
        public void TestaQuantidade() => Assert.AreEqual(0, context.Fabricante.Count());
        
    }
}
