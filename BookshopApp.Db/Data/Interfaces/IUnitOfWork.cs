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
        IUserRepository UsersRepository { get; }
        IOrderRepository OrdersRepository { get; }
        IProductRepository ProductsRepository { get; }
        IAuthorRepository AuthorsRepository { get; }

        /// <summary>
        /// My implemantation use try/catch in the method. Catch cantains context.DisposeAsync()
        /// </summary>
        /// <returns></returns>
        Task<bool> Commit();
        Task Rollback();
    }
}
