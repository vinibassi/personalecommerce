using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCadastrador.Models;

[assembly: HostingStartup(typeof(WebCadastrador.Areas.Identity.IdentityHostingStartup))]
namespace WebCadastrador.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<WebCadastradorContext>()
                .AddDefaultTokenProviders();
            });
        }
    }
}