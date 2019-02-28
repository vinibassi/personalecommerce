using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestesDeAceitacao.Pages.CarrinhoPages;
using TestesDeAceitacao.Pages.HomePages;
using TestesDeUnidade;
using WebCadastrador.Data;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Testes.CarrinhoTests
{

    [TestFixture]
    class LoginAteFinalizarPedido
    {
        private WebCadastradorContext context;
        private HomeIndex homePage;
        private CarrinhoIndex carrinhoPage;
        private Produto p;
        private Fabricante f;
        private FinalizaPedidoPage finalizaPedidoPage;
        private ItemCarrinhoAdicionado itemCarrinho;

        [OneTimeSetUp]
        public void Setup()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<WebCadastradorContext>()
               .UseLazyLoadingProxies()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCadastradorContext-dc88d854-cb2b-41f0-851e-fa57b037f7e8;Trusted_Connection=True;MultipleActiveResultSets=true");

            context = new WebCadastradorContext(builder.Options);
            context.Produto.Clear();
            context.Fabricante.Clear();
            context.SaveChanges();

            f = Generator.ValidFabricante();
            context.Add(f);
            context.SaveChanges();

            p = Generator.ValidProduto();
            p.Fabricante = f;
            context.Add(p);
            context.SaveChanges();

            homePage = new HomeIndex();
            carrinhoPage = new CarrinhoIndex();
            finalizaPedidoPage = new FinalizaPedidoPage();
            //act
            homePage.DeletaCookies();
            homePage.Navigate();
            homePage.AdicionarItemAoCarrinho();
            itemCarrinho = carrinhoPage.ItensDoCarrinho.Single();
            carrinhoPage.FinalizarPedido();
        }
        [Test]
        public void TestaNumeroDeItens() => Assert.AreEqual(1, finalizaPedidoPage.ItensPedidos.Count);
        [Test]
        public void TestaItemPedidoIgualItemCarrinho()
        {
            finalizaPedidoPage.ItensPedidos.Single().Should().BeEquivalentTo(new ItemPedidoAdicionado
            {
                Preco = itemCarrinho.Preco,
                Quantidade = itemCarrinho.Quantidade,
                Produto = itemCarrinho.Produto
            });
        }
        //[Test]
        //public void ItemCarrinhoIgualProdutoAdicionado() => itemCarrinho.Should().BeEquivalentTo(new ItemCarrinhoAdicionado
        //{
        //    Preco = p.Preco,
        //    Produto = p.Nome,
        //    Quantidade = 1
        //});
    }
}
