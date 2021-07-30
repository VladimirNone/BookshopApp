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

        public async Task<Order> GetUserCartAsync(int userId)
        {
            return await DbSet.Where(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsCart).Include(h => h.OrderedProducts).ThenInclude(h=>h.Product).FirstOrDefaultAsync();
        }
    }
}
