using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TestesDeAceitacao.Pages;
using WebCadastrador.Models;
using WebCadastrador.Controllers;
using TestesDeUnidade;
using WebCadastrador.ViewModels;
using WebCadastrador.Data;

namespace TestesDeAceitacao.Testes.FabricanteTests
{
    class CadastraFabricanteMesmoCNPJTeste
    {
        private WebCadastradorContext context;
        private NewFabricantePage page;
        private FabricantesViewModel novoFabricante;
        private FabricantesController controller;

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

            var f = Generator.ValidFabricante();
            context.Fabricante.Add(f);
            context.SaveChanges();

            page = new NewFabricantePage();
            novoFabricante = Generator.ValidFabricanteViewModel();
            novoFabricante.CNPJ = f.CNPJ;

            //act
            page.Navigate();
            page.Cadastra(novoFabricante);
        }
        [Test]
        public void QuantidadeDeFabricantes() => Assert.AreEqual(1, context.Fabricante.Count());
        [Test]
        public void MensagemDeErroApareceu() => Assert.AreEqual("Este CNPJ já está cadastrado.", page.LeCNPJError());
    }
}
