using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TestesDeAceitacao.Pages.CarrinhoPages;
using TestesDeAceitacao.Pages.HomePages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;
using WebCadastrador.Models.Carrinho;

namespace TestesDeAceitacao.Testes.CarrinhoTests
{
    [TestFixture]
    class TestaAddItemToCart
    {
        private WebCadastradorContext context;
        private HomeIndex homePage;
        private CarrinhoIndex carrinhoPage;
        private Produto p;

        [OneTimeSetUp]
        public void Setup()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
               .UseLazyLoadingProxies()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Produto.Clear();
            context.SaveChanges();
            p = Generator.ValidProduto();
            context.Add(p);
            context.SaveChanges();

            homePage = new HomeIndex();
            carrinhoPage = new CarrinhoIndex();
            //act
            homePage.Navigate();
            homePage.AdicionarItemAoCarrinho();
        }
        [Test]
        public void ItemCarrinhoFoiAdicionado() => carrinhoPage.ItensDoCarrinho.Single().Should().BeEquivalentTo(new ItemCarrinhoAdicionado
        {
            Preco = p.Preco,
            Produto = p.Nome,
            Quantidade = 1
        });
        [Test]
        public void ListaContemNumeroCertoDeItens() => Assert.AreEqual(1, carrinhoPage.ItensDoCarrinho.Count());
        //[Test]
        //public void TestaQuantidade()
        //{
        //    var a = carrinhoPage.ItensDoCarrinho.FirstOrDefault().Quantidade;
        //}
    }
}
