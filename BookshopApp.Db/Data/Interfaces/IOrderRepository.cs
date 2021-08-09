using BookshopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrCreateUserCartTracked(int userId);
        Task<(Order, bool)> GetOrCreateUserCartNoTracked(int userId, int page, int count);
        Task<(Order, bool)> GetOrderNoTracked(int orderId, int page, int count);
        Task<(List<Order>, bool)> GetOrdersNoTracked(int userId, int page, int count);
        Task<(List<Order>, bool)> GetOrdersNoTracked(int page, int count);

        Task CancelProductCart(int userId, int productId);
        Task AddToCart(int userId, int productId, int count);
        Task PlaceAnOrder(int userId);

        Task<Order> CreateCartTracked(int userId);
    }
}
