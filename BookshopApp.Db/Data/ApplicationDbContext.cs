using BookshopApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderState>().HasData(
                new OrderState() { Id = 1, NameOfState = "Корзина" },
                new OrderState() { Id = 2, NameOfState = "Подтвержден" },
                new OrderState() { Id = 3, NameOfState = "Завершен" },
                new OrderState() { Id = 4, NameOfState = "Отменен" }
                );

            modelBuilder.Entity<Product>().Property(h => h.LinkToImage).HasDefaultValue("/Images/no_foto.png");

            base.OnModelCreating(modelBuilder);
        }
    }
}
