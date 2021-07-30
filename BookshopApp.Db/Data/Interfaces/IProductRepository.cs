﻿using BookshopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product[]> GetProductsAsync(int page, int count);
        Task<Product> GetFullProductAsync(int id);
    }
}
