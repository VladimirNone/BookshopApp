using BookshopApp.Models;
using BookshopApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> ContainsUserByEmail(string email);
        public Task<bool> ContainsUserByUserName(string username);
    }
}
