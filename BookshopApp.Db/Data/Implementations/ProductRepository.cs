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

        public async Task<Product[]> GetProductsAsync(int page, int count)
            => await DbSet.Include(h=>h.Author).Where(h => h.Deleted == false).Skip(page * count).Take(count).AsNoTracking().ToArrayAsync();

        public override async Task<Product> GetEntityAsync(int id)
            => await DbSet.Include(h => h.Author).Where(h => h.Deleted == false).AsNoTracking().FirstOrDefaultAsync();
    }
}
