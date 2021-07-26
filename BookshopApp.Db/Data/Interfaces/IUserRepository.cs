using BookshopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> ContainsUserByEmail(string email);
        public Task<bool> ContainsUserByUserName(string username);
    }
}
