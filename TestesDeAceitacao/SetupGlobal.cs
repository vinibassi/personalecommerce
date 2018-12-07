using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using WebCadastradotr;

namespace TestesDeAceitacao
{
    [SetUpFixture]
    public class SetupGlobal
    {
        public static IWebDriver Driver { get; private set; }
        private static HttpWebAppFactory<Startup> factory;
        private ChromeOptions options;

        [OneTimeSetUp]
        public static void Setup()
        {
            //options = new ChromeOptions();
            //options.AddArgument("--headless");
            Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)/*, options*/);
            factory = new HttpWebAppFactory<Startup>();
            factory.CreateDefaultClient();
        }


        [OneTimeTearDown]
        public static void TearDown()
        {
            Driver?.Close();
            factory?.Dispose();
        }
    }
}
