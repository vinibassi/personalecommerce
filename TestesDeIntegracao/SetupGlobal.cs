using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using TestesDeIntegracao.Helpers;
using WebCadastradotr;

namespace TestesDeIntegracao
{
    [SetUpFixture]
    public class SetupGlobal
    {
        public static WebApplicationFactory<Startup> factory { get; private set; }

        public static HttpClient HttpClient { get; private set; }

        [OneTimeSetUp]
        public static async Task SetupAsync()
        {
            factory = new InMemoryWebAppFactory<Startup>();
            HttpClient = factory.CreateClient();
            await LoginAsync();
        }

        private static async Task LoginAsync()
        {
            var getResponse = await HttpClient.GetAsync("http://localhost/Identity/Account/Login");
            var content = await HtmlHelpers.GetDocumentAsync(getResponse);
            var response = await HttpClient.SendAsync(
               (IHtmlFormElement)content.QuerySelector("form[id='loginForm']"),
               (IHtmlButtonElement)content.QuerySelector("button[id='login']"),
               new Dictionary<string, string>{{"Input_Email","admin@admin.com"}, { "Input_Password", "Pass@123"}});
            response.EnsureSuccessStatusCode();
        }

        [OneTimeTearDown]
        public static void TearDown() => factory?.Dispose();
    }
}
