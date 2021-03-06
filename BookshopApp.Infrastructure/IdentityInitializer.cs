using BookshopApp.Db;
using BookshopApp.Infrastructure.Generator;
using BookshopApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Infrastructure
{
    public class IdentityInitializer
    {
        public static async Task InitializeAdminAndUserRolesAsync(RoleManager<Role> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new Role("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new Role("user"));
            }
        }

        public static async Task InitializeUserAsync(UserManager<User> userManager, string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null && await userManager.FindByNameAsync(email) == null)
            {
                var user = new User { Email = email, UserName = email };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        public static async Task InitializeContent(DataGenerator generator, UserManager<User> userManager, ApplicationDbContext context, string pathToImages)
        {
            var users = generator.GenerateUsers(8).ToList();

            foreach (var newUser in users)
                await userManager.CreateAsync(newUser, newUser.PasswordHash);

            users.AddRange(context.Users);

            var authors = generator.GenerateAuthors(10);
            var products = generator.GenerateProducts(35, pathToImages, authors.ToArray());
            var orders = generator.GenerateOrders(15, users.ToArray());
            var orderedProds = generator.GenerateOrderedProducts(100, orders.ToArray(), products.ToArray());

            foreach (var order in orders)
            {
                var localOrderProds = orderedProds.Where(h => h.Order.Id == order.Id).ToArray();
                order.FinalAmount = localOrderProds.Sum(h => h.Count * h.Product.Price);
            }

            await context.OrderedProducts.AddRangeAsync(orderedProds);
            await context.SaveChangesAsync();
        }
    }
}
