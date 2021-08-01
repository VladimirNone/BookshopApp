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

        /// <summary>
        /// My implemantation use try/catch in the method. Catch cantains context.Dispose()
        /// </summary>
        /// <returns></returns>
        Task<bool> Commit();
        Task Rollback();
    }
}
