using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using WebCadastrador.Models;
using TestesDeAceitacao.Pages.FabricantePages;
using TestesDeUnidade;
using WebCadastrador.Data;
using FluentAssertions;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class ReadFabricanteTest
    {
        private FabricanteCadastrado fabricante;
        private WebCadastradorContext context;
        private Fabricante f;
        private FabricanteListPage page;

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
            context.Fabricante.Add(f);
            context.SaveChanges();
            page = new FabricanteListPage();
         
            //act
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Fabricantes");
        }
        [Test]
        public void ListaContemFabricanteCerto() => page.Fabricante.Single().Should().BeEquivalentTo(new FabricanteCadastrado
        {
            Nome = f.Nome,
            CNPJ = Formatador.CNPJ(f.CNPJ),
            Endereco = f.Endereco
        });
        [Test]
        public void ListaContemNumeroCertoDeFabricantes() => Assert.AreEqual(1, context.Fabricante.Count());
    }
}