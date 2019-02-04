using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages.FabricantePages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class UpdateFabricanteTest
    {
        private Fabricante fabricanteCadastrado;
        private WebCadastradorContext context;
        private FabricantesViewModel fabricanteEditado;

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

            var f = Generator.ValidFabricante();
            context.Fabricante.Add(f);
            context.SaveChanges();

            fabricanteEditado = Generator.ValidFabricanteViewModel();
            fabricanteEditado.CNPJ = f.CNPJ;
            var page = new UpdateFabricantePage();
            var id = context.Fabricante.First().Id;
            //act
            page.GoToAndLogin();
            page.NavegaToEdit(id);
            page.ModificaFabricante(fabricanteEditado);
            context = new WebCadastradorContext(builder.Options);
            fabricanteCadastrado = context.Fabricante.First();
        }
        [Test]
        public void QuantidadeDeFabricantes() => Assert.AreEqual(1, context.Fabricante.Count());
        [Test]
        public void TestaNewEndereco() => Assert.AreEqual(fabricanteEditado.Endereco, fabricanteCadastrado.Endereco);
    }
}
