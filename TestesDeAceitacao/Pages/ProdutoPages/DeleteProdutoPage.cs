using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TestesDeAceitacao.Pages;
using TestesDeAceitacao.Pages.FabricantePages;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Pages.ProdutoPages
{
    class DeleteProdutoPage
    {
        public void NavigateToDeletePage(int id)
        {
            SetupGlobal.Driver.Navigate().GoToUrl($"https://localhost:5001/Produtos/Delete/{id}");
        }
        public void DeletaFabricante()
        {
            SetupGlobal.Driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();
        }
    }
}
