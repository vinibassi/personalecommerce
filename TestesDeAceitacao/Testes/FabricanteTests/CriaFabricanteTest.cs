using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class CriaFabricanteTest
    {
        private Fabricante fabricante;
        private WebCadastradorContext context;
        private FabricantesViewModel novoFabricante;

        // private NewFabricantePage page;
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
            context.SaveChanges();

            var page = new NewFabricantePage();
            novoFabricante = Generator.ValidFabricanteViewModel();
            //act

            page.GoToAndLogin();
            page.Navigate();
            page.Cadastra(novoFabricante);
            fabricante = context.Fabricante.FirstOrDefault();
        }
        [Test]
        public void QuantidadeDeFabricantes() => Assert.AreEqual(1, context.Fabricante.Count());
        [Test]
        public void TestaNome() => Assert.AreEqual(novoFabricante.Nome, fabricante.Nome);
        [Test]
        public void TestaCNPJ() => Assert.AreEqual(novoFabricante.CNPJ, fabricante.CNPJ);
        [Test]
        public void TestaEndereco() => Assert.AreEqual(novoFabricante.Endereco, fabricante.Endereco);
    }
}
