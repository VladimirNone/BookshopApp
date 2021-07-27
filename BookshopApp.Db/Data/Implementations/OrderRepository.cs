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

        public async Task<Order> GetUserBasketAsync(int userId)
        {
            //can i use asnotracking here?
            AppDbContext.Products.Load();
            return await DbSet.Include(h => h.OrderedProducts).Where(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsBasket).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
