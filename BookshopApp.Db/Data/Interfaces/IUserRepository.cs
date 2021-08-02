using BookshopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public interface IUserRepository : IRepository<User>
    {
        Task AddDiscount(int userId, Discount discount);
        Task<Discount> GetDiscount(int userId);
        Task DecreaseDiscountNumbOfUses(int userId);
    }
}
