using BookshopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        //.Take(count+1) for determine - Is the page the last?
        public async Task<Product[]> GetProductsAsync(int page, int count)
            => await DbSet.Where(h => h.Deleted == false).Skip(page * count).Take(count+1).Include(h => h.Author).AsNoTracking().ToArrayAsync();

        public async Task<Product> GetFullProductAsync(int id)
            => await DbSet.Where(h => h.Deleted == false && h.Id == id).Include(h => h.Author).AsNoTracking().SingleOrDefaultAsync();
    }
}
