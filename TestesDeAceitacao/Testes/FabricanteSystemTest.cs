using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TestesDeAceitacao.Pages;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeAceitacao.Testes
{
    class FabricanteSystemTest
    {
        [Test]
        public void CadastraFabricante()
        {
            var page = new NewFabricantePage();
            page.Visita();
            page.Cadastra("Bassi LTDA", "94170922000190", "Rua abcdxyz, 23");
        }
    }
}
