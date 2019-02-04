using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCadastrador.Areas.Identity.Data;

namespace WebCadastradotr
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
                await CreateRolesandAddToUsersAsync(scope.ServiceProvider);
            await webHost.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        private async static Task CreateRolesandAddToUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();

            await CreateRole(serviceProvider, "Admin");
            await CreateRole(serviceProvider, "Manager");
            await CreateRole(serviceProvider, "Employee");
            
            await AddNewUserToRoleAsync(serviceProvider, "admin@admin.com", "Admin");
            await AddNewUserToRoleAsync(serviceProvider, "manager@manager.com", "Manager");
            await AddNewUserToRoleAsync(serviceProvider, "employee@employee.com", "Employee");
        }

        private static async Task CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();

            if (!roleExists)
            {
                var role = new AppRole
                {
                    Name = roleName
                };
                var createEmployeeRoleResult = await roleManager.CreateAsync(role);
                LogError(logger, createEmployeeRoleResult, $"Error creating role {roleName}:");
            }
        }

        private static async Task AddNewUserToRoleAsync(IServiceProvider serviceProvider, string email, string role)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();

            if ((await userManager.FindByEmailAsync(email)) == null)
            {
                var user = new AppUser
                {
                    UserName = email,
                    Email = email
                };

                var userResult = await userManager.CreateAsync(user, "Pass@123");
                if (userResult.Succeeded)
                {
                    var addToRoleResult = await userManager.AddToRoleAsync(user, role);
                    LogError(logger, addToRoleResult, "Error adding user to role:");
                }
                else LogError(logger, userResult, "Error creating user:");
            }
        }

        private static void LogError(ILogger<Startup> logger, IdentityResult result, string msg)
        {
            if (!result.Succeeded)
            {
                logger.LogCritical($"{msg}\n{result.Errors.Aggregate("", (acc, e) => e.Description + "\n")}");
            }
        }

    }
}
