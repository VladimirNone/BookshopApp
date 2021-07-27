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

        //test .AsNoTracking()
        public async Task<Order> GetUserBasket(int userId)
        {
            AppDbContext.Products.Load();
            return await DbSet.Include(h => h.OrderedProducts).AsNoTracking().SingleOrDefaultAsync(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsBasket);
        }
    }
}
