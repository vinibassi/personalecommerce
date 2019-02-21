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
            GoToAndLogin();
        }
        public static void GoToAndLogin()
        {
            Driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            ChecaCookiePolicy();
            Driver.FindElement(By.Id("Input_Email")).SendKeys("admin@admin.com");
            Driver.FindElement(By.Id("Input_Password")).SendKeys("Pass@123");
            Driver.FindElement(By.Id("login")).Click();
        }
        public static void ChecaCookiePolicy()
        {
            var cookieButton = Driver.FindElement(By.CssSelector("#cookieConsent > div > div.collapse.navbar-collapse > div > button"));
            if(cookieButton.Displayed)
                cookieButton.Click();
            else
                return;
        }
        [OneTimeTearDown]
        public static void TearDown()
        {
            Driver?.Close();
            factory?.Dispose();
        }
    }
}
