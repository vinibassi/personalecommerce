using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using WebCadastrador.Models;
using WebCadastrador.Controllers;
using TestesDeAceitacao.Pages.FabricantePages;
using TestesDeUnidade;
using WebCadastrador.ViewModels;
using WebCadastrador.Data;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class EditaFabricanteCNPJExistente
    {
        private DbContextOptionsBuilder<WebCadastradorContext> builder;
        private WebCadastradorContext context;
        private UpdateFabricantePage page;
        private FabricantesViewModel novoFabricante;
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

            fabricante1 = Generator.ValidFabricante();
            context.Fabricante.Add(fabricante1);

            var f2 = Generator.ValidFabricante();
            context.Fabricante.Add(f2);

            context.SaveChanges();

            page = new UpdateFabricantePage();
            novoFabricante = Generator.ValidFabricanteViewModel();
            novoFabricante.CNPJ = f2.CNPJ;

            //ACT
            page.GoToAndLogin();
            page.NavegaToEdit(fabricante1.Id);
            page.ModificaFabricante(novoFabricante);

        }
        [Test]
        public void CNPJNaoMudou()
        {
            context = new WebCadastradorContext(builder.Options);
            var cliente = context.Fabricante.First(c => c.Id == fabricante1.Id);
            Assert.AreEqual(fabricante1.CNPJ, fabricante1.CNPJ);
        }
        [Test]
        public void TestaURL() => Assert.AreEqual($"https://localhost:5001/Fabricantes/Edit/{fabricante1.Id}", page.Url);
        [Test]
        public void MensagemDeErroApareceu() => Assert.AreEqual("Este CNPJ já está cadastrado.", page.LeCnpjError());
    }
}
