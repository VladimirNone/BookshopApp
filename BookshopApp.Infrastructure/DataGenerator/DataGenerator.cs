using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using BookshopApp.Db;
using BookshopApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BookshopApp.Infrastructure.Generator
{
    public class DataGenerator
    {
        readonly ApplicationDbContext _context;

        public DataGenerator(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GenerateProducts(int count, string pathToDirWithImages)
            => ObjectGenerator.GenerateProduct(_context.Authors.ToArray(), pathToDirWithImages).Generate(count);

        public IEnumerable<Product> GenerateProducts(int count, string pathToDirWithImages, Author[] authors)
            => ObjectGenerator.GenerateProduct(authors, pathToDirWithImages).Generate(count);

        public IEnumerable<User> GenerateUsers(int count)
            => ObjectGenerator.GenerateUser().Generate(count);

        public IEnumerable<OrderedProduct> GenerateOrderedProducts(int count)
            => ObjectGenerator.GenerateOrderedProduct(_context.Orders.ToArray(), _context.Products.ToArray()).Generate(count);

        public IEnumerable<OrderedProduct> GenerateOrderedProducts(int count, Order[] orders, Product[] products)
            => ObjectGenerator.GenerateOrderedProduct(orders, products).Generate(count);

        public IEnumerable<Order> GenerateOrders(int count)
            => ObjectGenerator.GenerateOrder(_context.Users.ToArray()).Generate(count);

        public IEnumerable<Order> GenerateOrders(int count, User[] users)
            => ObjectGenerator.GenerateOrder(users).Generate(count);

        public IEnumerable<Author> GenerateAuthors(int count)
            => ObjectGenerator.GenerateAuthor().Generate(count);
    }
}
