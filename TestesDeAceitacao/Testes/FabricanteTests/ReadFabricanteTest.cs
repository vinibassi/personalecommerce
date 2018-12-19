using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using WebCadastrador.Models;
using TestesDeAceitacao.Pages.FabricantePages;
using TestesDeUnidade;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class ReadFabricanteTest
    {
        private FabricanteCadastrado fabricante;
        private WebCadastradorContext context;
        private Fabricante f;

        [OneTimeSetUp]
        public void ModificaCliente()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");
            context = new WebCadastradorContext(builder.Options);
            context.Produto.Clear();
            context.Fabricante.Clear();

            f = Generator.ValidFabricante();
            f.CNPJ = "94170922000190";
            context.Fabricante.Add(f);
            context.SaveChanges();
            var page = new FabricanteListPage();
            //act
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Fabricantes");
            fabricante = page.Fabricante.FirstOrDefault(c => c.CNPJ == "94.170.922/0001-90");
        }
        [Test]
        public void ReadFabricantes() => Assert.AreEqual(f.Nome, fabricante.Nome);
    }
}