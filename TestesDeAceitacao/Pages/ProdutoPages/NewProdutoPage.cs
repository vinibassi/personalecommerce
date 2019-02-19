using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebCadastrador.Models;

namespace TestesDeAceitacao.Pages.ProdutoPages
{
    class NewProdutoPage
    {
        public string Url => SetupGlobal.Driver.Url; 
        public void Visita()
        {
            SetupGlobal.Driver.Navigate().GoToUrl("https://localhost:5001/Produtos/Create");
        }
        public void Cadastra(Produto novoProduto)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeProduto = driver.FindElement(By.Id("Nome"));
            IWebElement precoProduto = driver.FindElement(By.Id("Preco"));
            IWebElement produtoFabricante = driver.FindElement(By.Id("Fabricante"));
            IWebElement produtoFoto = driver.FindElement(By.Id("Url"));
            var selectElement = new SelectElement(produtoFabricante);
            selectElement.SelectByValue(novoProduto.Fabricante.Id.ToString());
            nomeProduto.SendKeys(novoProduto.Nome);
            precoProduto.SendKeys(novoProduto.Preco.ToString());
            produtoFoto.SendKeys("https://www.catster.com/wp-content/uploads/2017/08/A-fluffy-cat-looking-funny-surprised-or-concerned.jpg");
            nomeProduto.Submit();
        }
        public string LeUrlErro()
        {
            var driver = SetupGlobal.Driver;
            var erro = driver.FindElement(By.CssSelector("#Url-error"));
            return erro.Text;
        }
        public void CadastraProdutoInvalido(Produto novoProduto)
        {
            var driver = SetupGlobal.Driver;
            IWebElement nomeProduto = driver.FindElement(By.Id("Nome"));
            IWebElement precoProduto = driver.FindElement(By.Id("Preco"));
            IWebElement produtoFabricante = driver.FindElement(By.Id("Fabricante"));
            IWebElement produtoFoto = driver.FindElement(By.Id("Url"));
            var selectElement = new SelectElement(produtoFabricante);
            selectElement.SelectByValue(novoProduto.Fabricante.Id.ToString());
            nomeProduto.SendKeys(novoProduto.Nome);
            precoProduto.SendKeys(novoProduto.Preco.ToString());
            produtoFoto.SendKeys(novoProduto.FotoUrl);
            nomeProduto.Submit();
        }
    }
}
