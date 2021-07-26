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
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<bool> ContainsUserByEmail(string email)
        {
            return await DbSet.AnyAsync(h => h.Email == email);
        }

        public async Task<bool> ContainsUserByUserName(string username)
        {
            return await DbSet.AnyAsync(h => h.UserName == username);
        }
    }
}
