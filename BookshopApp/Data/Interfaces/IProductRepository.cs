using BookshopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Data.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
