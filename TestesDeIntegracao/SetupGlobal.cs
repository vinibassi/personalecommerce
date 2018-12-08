using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using WebCadastradotr;

namespace TestesDeIntegracao
{
    [SetUpFixture]
    public class SetupGlobal
    {
        public static WebApplicationFactory<Startup> factory { get; private set; }

        public static HttpClient HttpClient { get; private set; }

        [OneTimeSetUp]
        public static void Setup()
        {
            factory = new InMemoryWebAppFactory<Startup>();
            HttpClient = factory.CreateDefaultClient();
        }

        [OneTimeTearDown]
        public static void TearDown() => factory?.Dispose();
    }
}
