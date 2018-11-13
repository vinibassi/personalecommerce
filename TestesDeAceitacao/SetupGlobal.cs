using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using WebCadastradotr;

namespace TestesDeAceitacao
{
    [SetUpFixture]
    public class SetupGlobal
    {
        public static IWebDriver Driver { get; private set; }
        private ChromeOptions options;

        [OneTimeSetUp]
        public static void Setup()
        {
            //options = new ChromeOptions();
            //options.AddArgument("--headless");
            Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)/*, options*/);
            //var factory = new WebApplicationFactory<Startup>();
        }


        [OneTimeTearDown]
        public static void TearDown()
        {
            Driver?.Close();
        }
    }
}
