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

        /// <summary>
        /// Take(count+1) for determine - Is the page the last?
        /// For front-end display for user. Used .AsNoTracking()
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns>count + 1 Products Items. Start from page * count</returns>
        public async Task<Product[]> GetProductsAsync(int page, int count)
            => await DbSet.Where(h => !h.Deleted).Skip(page * count).Take(count+1).Include(h => h.Author).AsNoTracking().ToArrayAsync();

        /// <summary>
        /// For front-end display for user. Used .AsNoTracking()
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetFullProductAsync(int id)
            => await DbSet.Where(h => !h.Deleted && h.Id == id).Include(h => h.Author).AsNoTracking().SingleOrDefaultAsync();
    }
}
