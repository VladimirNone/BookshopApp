using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using BookshopApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Infrastructure.Generator
{
    public class ObjectGenerator
    {
        public static Faker<Product> GenerateProduct(Author[] publishers, string pathToDirWithImages)
            => new Faker<Product>("ru")
                .RuleFor(h => h.CountInStock, g => g.Random.Number(0, 1000))
                .RuleFor(h => h.Description, g => g.Lorem.Paragraph())
                .RuleFor(h => h.LinkToImage, g => "/Images/" + g.Random.Number(0, Directory.GetFiles(pathToDirWithImages, "*.jpg").Length - 1)+".jpg")
                .RuleFor(h => h.Name, g => g.Commerce.ProductName())
                .RuleFor(h => h.Price, g => Math.Round(g.Random.Double() + g.Random.Number(0, 1000), 2))
                .RuleFor(h => h.Author, g => g.Random.ArrayElement(publishers))
                .RuleFor(h => h.YearOfRelease, g => g.Random.Short(0, (short)DateTime.Now.Year))
                .RuleFor(h => h.DateOfPublication, (g, o) => g.Date.Between(DateTime.Parse("1990.05.05"), DateTime.Now));

        public static Faker<OrderedProduct> GenerateOrderedProduct(Order[] orders, Product[] products)
            => new Faker<OrderedProduct>("ru")
                .RuleFor(h => h.Product, g => g.Random.ArrayElement(products))
                .RuleFor(h => h.Order, g => g.Random.ArrayElement(orders))
                .RuleFor(h => h.Count, g => g.Random.Number(1,10))
                .RuleFor(h => h.TimeOfBuing, (g, o) => g.Date.Between(products.First(j=>j.Id == o.Product.Id).DateOfPublication, DateTime.Now));

        public static Faker<Order> GenerateOrder(User[] customers)
            => new Faker<Order>("ru")
                .RuleFor(h => h.Customer, g => g.Random.ArrayElement(customers))
                .RuleFor(h => h.DateOfOrdering, g => g.Date.Between(DateTime.Parse("1990.05.05"), DateTime.Now))
                .RuleFor(h => h.DateOfClosing, (g, o) => g.Date.Between(o.DateOfOrdering, DateTime.Now))
                .RuleFor(h => h.FinalLocation, g=>g.Address.Locale)
                .RuleFor(h => h.StateId, g => (int)OrderStateEnum.Completed);

        public static Faker<User> GenerateUser()
            => new Faker<User>("ru")
                .RuleFor(h => h.DateOfRegistration, g => g.Date.Between(new DateTime(2015, 10, 10), DateTime.Now))
                .RuleFor(h => h.Email, g => g.Person.Email)
                .RuleFor(h => h.UserName, g => g.Person.UserName)
                .RuleFor(h => h.PasswordHash, g => g.Internet.Password());
                //.FinishWith(async (g, o) => await userManager.CreateAsync(o, g.Internet.Password()));

        public static Faker<Author> GenerateAuthor()
            => new Faker<Author>("ru")
                .RuleFor(h => h.FirstName, g => g.Person.FirstName)
                .RuleFor(h => h.LastName, g => g.Person.LastName);
    }
}
