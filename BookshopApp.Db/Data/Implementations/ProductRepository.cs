using BookshopApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Take(count+1) for determine - Is the page the last?
        /// For front-end display for user. Used .AsNoTracking()
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns>count + 1 Products Items. Start from page * count</returns>
        public async Task<(List<Product>, bool)> GetProductsNoTracked(int page, int count)
        {
            var prods = await DbSet
                .Where(h => !h.Deleted)
                .Include(h => h.Author)
                .Skip(page * count)
                .Take(count + 1)
                .AsNoTracking()
                .ToListAsync();

            //we return count items, but for determining - Is this page the last? - we use this condition 
            //if prods.Count() == (count + 1) then exist next page
            var pageIsLast = prods.Count <= count;

            if (!pageIsLast)
                prods.Remove(prods.Last());

            return (prods, pageIsLast);
        }

        /// <summary>
        /// For front-end display for user. Used .AsNoTracking()
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetFullProductNoTracked(int id)
            => await DbSet.Where(h => !h.Deleted && h.Id == id).Include(h => h.Author).AsNoTracking().SingleOrDefaultAsync();
    }
}
