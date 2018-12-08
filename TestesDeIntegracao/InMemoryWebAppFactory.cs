using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestesDeIntegracao
{
    public class InMemoryWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup:class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            base.ConfigureWebHost(builder);
        }
    }
}
