using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestesDeAceitacao
{
    public class HttpWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup:class
    {
        private IWebHost host;

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseStartup<TStartup>();
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            host = builder.Build();
            host.Start();
            return new TestServer(new WebHostBuilder().UseStartup<TStartup>());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing) host?.Dispose();
        }
    }
}
