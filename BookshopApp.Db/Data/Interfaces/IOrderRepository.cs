using BookshopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrCreateUserCart(int userId);
        Task<(Order, bool)> GetOrCreateUserCart(int userId, int page, int count);
        Task<(List<Order>, bool)> GetOrders(int userId, int page, int count);

        Task CancelProductCart(int userId, int productId);
        Task AddToCart(int userId, int productId, int count);
        Task PlaceAnOrder(int userId);

        Task<Order> CreateCart(int userId);
    }
}
