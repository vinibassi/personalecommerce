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
                await CreateRolesandUsersAsync(scope.ServiceProvider);
            await webHost.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        private async static Task CreateRolesandUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();
            bool roleExists = await roleManager.RoleExistsAsync("Admin");
            if (!roleExists)
            {
                // first we create Admin rool    
                var role = new AppRole();
                role.Name = "Admin";
                var createRoleResult = await roleManager.CreateAsync(role);
                LogError(logger, createRoleResult, "Error creating role admin:");
            }
            //Here we create a Admin super user who will maintain the website                   

            var user = new AppUser
            {
                UserName = "default",
                Email = "default@default.com"
            };

            var userResult = await userManager.CreateAsync(user, "Admin@123");

            //Add default User to Role Admin    
            if (userResult.Succeeded)
            {
                var addToRoleResult = await userManager.AddToRoleAsync(user, "Admin");
                LogError(logger, addToRoleResult, "Error adding user to role:");
            }
            else
            {
                LogError(logger, userResult, "Error creating user:");
            }


            //Creating Manager role     
            roleExists = await roleManager.RoleExistsAsync("Manager");
            if (!roleExists)
            {
                var role = new AppRole
                {
                    Name = "Manager"
                };
                var createManagerRoleResult = await roleManager.CreateAsync(role);
                LogError(logger, createManagerRoleResult, "Error creating role manager:");
            }

            //Creating Employee role     
            roleExists = await roleManager.RoleExistsAsync("Employee");
            if (!roleExists)
            {
                var role = new AppRole
                {
                    Name = "Employee"
                };
                var createEmployeeRoleResult = await roleManager.CreateAsync(role);
                LogError(logger, createEmployeeRoleResult, "Error creating role employee:");
            }
        }

        private static void LogError(ILogger<Startup> logger, IdentityResult createRoleResult, string msg)
        {
            if (!createRoleResult.Succeeded)
            {
                logger.LogCritical($"{msg}\n{createRoleResult.Errors.Aggregate("", (acc, e) => e.Description + "\n")}");
            }
        }

    }
}
