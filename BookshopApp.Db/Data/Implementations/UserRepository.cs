using BookshopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {
        }

        public async Task AddDiscount(int userId, Discount discount)
        {
            (await GetEntityAsync(userId)).Discount = discount;
        }

        public async Task DecreaseDiscountNumbOfUses(int userId)
        {
            var discount = (await DbSet.Include(h => h.Discount).SingleOrDefaultAsync(h => h.Id == userId)).Discount;
            if (discount?.NumberOfUses > 0)
                discount.NumberOfUses--;
        }

        public async Task<Discount> GetDiscountNoTracked(int userId)
            => (await DbSet.Include(h => h.Discount).AsNoTracking().SingleOrDefaultAsync(h=> h.Id == userId)).Discount;
        
    }
}
