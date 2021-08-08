using BookshopApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<(List<Product>, bool)> GetProducts(int page, int count);
        Task<Product> GetFullProduct(int id);
        Task<string> SaveImage(string contentRootPath, IFormFile imageFile);
    }
}
