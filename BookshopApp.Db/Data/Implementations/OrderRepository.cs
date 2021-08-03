using BookshopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Db.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {

        }

        public async Task CancelProductCart(int userId, int productId)
        {
            var cart = await DbSet
                .Where(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsCart)
                .Include(h => h.OrderedProducts.Where(h => h.ProductId == productId))
                .ThenInclude(h => h.Product)
                .FirstOrDefaultAsync();

            var cancelledProd = cart.OrderedProducts.First();

            cart.FinalAmount -= cancelledProd.Count * cancelledProd.Product.Price;
            cancelledProd.Cancelled = true;
        }

        public async Task AddToCart(int userId, int productId, int count)
        {
            var cart = await GetOrCreateUserCart(userId);
            var orderedProduct = cart.OrderedProducts.Find(h => !h.Cancelled && h.ProductId == productId);
            if (orderedProduct == null)
            {
                cart.OrderedProducts.Add(new OrderedProduct() { ProductId = productId, Count = count, OrderId = cart.Id, TimeOfBuing = DateTime.Now });

                var prod = await UnitOfWork.ProductsRepository.GetEntityAsync(productId);
                prod.CountInStock -= count;
                cart.FinalAmount += count * prod.Price;
            }
            else
            {
                orderedProduct.Count += count;
                orderedProduct.Product.CountInStock -= count;
                cart.FinalAmount += count * orderedProduct.Product.Price;
            }

            var discount = await UnitOfWork.UsersRepository.GetDiscount(userId);
            if (discount != null && discount.NumberOfUses != 0) {
                cart.FinalAmount -= (cart.FinalAmount / 100) * discount.Percent;
                discount.NumberOfUses--;
            }
        }

        public async Task PlaceAnOrder(int userId)
        {
            var cart = await DbSet
                .Where(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsCart)
                .FirstOrDefaultAsync();
            
            cart.StateId = (int)OrderStateEnum.Confirmed;
            cart.DateOfOrdering = DateTime.Now;

            await UnitOfWork.UsersRepository.DecreaseDiscountNumbOfUses(userId);

            if(cart.FinalAmount > 500)
                await UnitOfWork.UsersRepository.AddDiscount(userId, new Discount() { Percent = 5, NumberOfUses = 1 });
        }

        /// <summary>
        /// For back-end. Tracked
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Order> GetOrCreateUserCart(int userId)
        {
            var cartExist = await DbSet.Where(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsCart).AsNoTracking().AnyAsync();
            if(!cartExist)
                return await CreateCart(userId);

            return await DbSet.Where(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsCart).Include(h => h.OrderedProducts).ThenInclude(h => h.Product).FirstOrDefaultAsync();
        }

        /// <summary>
        /// For front-end display for user. Used .AsNoTracking() 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns>(cart with 'count' of OrderedProduct Items, pageIsLast) </returns>
        // Don't remove .AsNoTracking() //
        public async Task<(Order, bool)> GetOrCreateUserCart(int userId, int page, int count)
        {
            var cart = await DbSet
                .Where(h => h.CustomerId == userId && h.StateId == (int)OrderStateEnum.IsCart)
                .Include(h => h.OrderedProducts.Where(h => !h.Cancelled).OrderBy(h => h.Product.Name).Skip(page * count).Take(count + 1))
                .ThenInclude(h => h.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if(cart == null)
                cart = await CreateCart(userId);

            var pageIsLast = cart.OrderedProducts.Count <= count;

            if (!pageIsLast)
                cart.OrderedProducts.Remove(cart.OrderedProducts.Last());

            return (cart, pageIsLast);
        }

        public async Task<Order> CreateCart(int userId)
        {
            var cart = new Order() { CustomerId = userId, StateId = (int)OrderStateEnum.IsCart, OrderedProducts = new List<OrderedProduct>() };

            await AddEntityAsync(cart);

            return cart;
        }

        public async Task<(List<Order>, bool)> GetOrders(int userId, int page, int count)
        {
            var orders = await DbSet
                .Where(h => h.CustomerId == userId && h.StateId != (int)OrderStateEnum.IsCart)
                .Include(h => h.State)
                .Skip(page * count)
                .Take(count + 1)
                .AsNoTracking()
                .ToListAsync();

            //we return count items, but for determining - Is this page the last? - we use this condition 
            //if orders.Count() == (count + 1) then exist next page
            var pageIsLast = orders.Count <= count;

            if (!pageIsLast)
                orders.Remove(orders.Last());

            return (orders, pageIsLast);
        }
    }
}
