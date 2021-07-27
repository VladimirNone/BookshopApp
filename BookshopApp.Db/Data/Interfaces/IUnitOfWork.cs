using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public interface IUnitOfWork
    {
        public IUserRepository UsersRepository { get; }
        public IOrderRepository OrdersRepository { get; }
        public IProductRepository ProductsRepository { get; }

        Task<bool> Commit();
        Task Rollback();
    }
}
