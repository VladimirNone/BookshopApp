using BookshopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// For back-end. Tracked
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Order> GetUserCartAsync(int userId)
            => await DbSet.Where(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsCart).Include(h => h.OrderedProducts).ThenInclude(h=>h.Product).FirstOrDefaultAsync();

        /// <summary>
        /// For front-end display for user. Used .AsNoTracking() 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns>count + 1 OrderedProduct Items. Start from page * count</returns>
        // Don't remove .AsNoTracking() //
        public async Task<Order> GetUserCartAsync(int userId, int page, int count)
            => await DbSet.Where(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsCart)
                .Include(h => h.OrderedProducts.Where(h => !h.Cancelled).OrderBy(h=>h.Product.Name).Skip(page * count).Take(count + 1))
                .ThenInclude(h => h.Product) 
                .AsNoTracking()
                .FirstOrDefaultAsync();
    }
}
