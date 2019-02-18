﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCadastrador.Areas.Identity.Data;
using WebCadastrador.Models;
using WebCadastrador.Models.Carrinho;
using WebCadastrador.Models.Repositories;

namespace WebCadastrador.Controllers
{
    public class CarrinhoController : Controller
    {
        private IProdutoRepository produtoRepository;
        private IPedidoRepository pedidoRepository;
        private UserManager<AppUser> userManager;
        public CarrinhoController(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository, UserManager<AppUser> userManager)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var idProdutosDoCarrinho = HttpContext.Session.Get<List<int>>("carrinho");
            if (idProdutosDoCarrinho == null)
                return View(new Carrinho());
            var produtos = await Task.WhenAll(idProdutosDoCarrinho.Select(id => produtoRepository.FindProdutoByIdAsync(id)));
            var itens = produtos.Select(produto =>
                new ItemCarrinho()
                {
                    Preco = produto.Preco,
                    Produto = produto, 
                    Quantidade = 1
                });
            return View(new Carrinho { Produtos = itens });
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var carrinho = HttpContext.Session.Get<List<int>>("carrinho") ?? new List<int>();
            carrinho.Add(id);
            HttpContext.Session.Set("carrinho", carrinho);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> FinalizarPedido()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var idProdutos = HttpContext.Session.Get<List<int>>("carrinho");
            var produtos = await Task.WhenAll(idProdutos.Select(id => produtoRepository.FindProdutoByIdAsync(id)));
            var pedido = new Pedido();
            pedidoRepository.CriaPedido(pedido);
            pedido.AdicionarItens(produtos.ToList());
            pedido = pedidoRepository.FindById(pedido.Id);
            return View((pedido, user));
        }


    }
}