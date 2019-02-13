using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCadastrador.Models;
using WebCadastrador.Models.Repositories;
using WebCadastradotr.Models;

namespace WebCadastradotr.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoRepository produtoRepositorio;
        public HomeController(IProdutoRepository produtoRepositorio)
        {
            this.produtoRepositorio = produtoRepositorio;
        }
        public async Task<IActionResult> Index()
        {
            return View(await produtoRepositorio.ListaProdutosAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await produtoRepositorio.FindProdutoByIdAsync(id.Value);
            if (produto == null)
            {
                return NotFound();
            }

            var p = new ProdutoDetailsVMUser
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Fabricante = produto.Fabricante,
                Id = produto.Id,
                Url = produto.FotoUrl
            };

            return View(p);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
