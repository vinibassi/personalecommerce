using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using WebCadastrador.Areas.Identity.Data;
using WebCadastrador.Data;
using WebCadastrador.Models;

[assembly: HostingStartup(typeof(WebCadastrador.Areas.Identity.IdentityHostingStartup))]
namespace WebCadastrador.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<WebCadastradorContext>()
                .AddDefaultTokenProviders();

                services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/Identity/Account/Login";
                    options.LogoutPath = $"/Identity/Account/Logout";
                    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                });

                services.AddSingleton<IEmailSender, EmailSender>();

            });
        }
    }
}