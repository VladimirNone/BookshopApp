using BookshopApp.Db;
using BookshopApp.Db.Generator;
using BookshopApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //await SeedDatabase(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task SeedDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<Role>>();

                    await IdentityInitializer.InitializeAdminAndUserRolesAsync(rolesManager);
                    await IdentityInitializer.InitializeUserAsync(userManager, "admin@gmail.com", "123456", "admin");
                    await IdentityInitializer.InitializeUserAsync(userManager, "user@gmail.com", "123456", "user");

                    //Wiil create a new objects for each application launch. Need comment it
                    /*                    var dataGenerator = services.GetRequiredService<DataGenerator>();
                                        var appDbContext = services.GetRequiredService<ApplicationDbContext>();
                                        var env = services.GetRequiredService<IWebHostEnvironment>();

                                        await IdentityInitializer.InitializeContent(dataGenerator, userManager, appDbContext, Path.Combine(env.ContentRootPath, "ClientApp", "Public", "Images"));*/
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }
}
