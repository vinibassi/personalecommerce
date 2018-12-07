using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class CriaFabricanteTest
    {
        private Fabricante fabricante;
        private WebCadastradorContext context;
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
            //act
            var page = new NewFabricantePage();
            page.Navigate();
            page.Cadastra("Bassi LTDA","94170922000190","Rua abcdxyz, 23");
            fabricante = context.Fabricante.FirstOrDefault();
        }
        [Test]
        public void QuantidadeDeFabricantes() => Assert.AreEqual(1, context.Fabricante.Count());
        [Test]
        public void TestaNome() => Assert.AreEqual("Bassi LTDA", fabricante.Nome);
        [Test]
        public void TestaCNPJ() => Assert.AreEqual("94170922000190", fabricante.CNPJ);
        [Test]
        public void TestaEndereco() => Assert.AreEqual("Rua abcdxyz, 23", fabricante.Endereco);
    }
}
