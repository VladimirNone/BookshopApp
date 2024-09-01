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
        Task<(List<Product>, bool)> GetProductsNoTracked(int page, int count);
        Task<Product> GetFullProductNoTracked(int id);
    }
}
