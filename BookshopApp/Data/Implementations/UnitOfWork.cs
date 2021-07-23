using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookshopApp.Data.Interfaces;
using BookshopApp.Data;

namespace BookshopApp.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _usersRepository;
        private IOrderRepository _ordersRepository;
        private IProductRepository _productsRepository;
        private ApplicationDbContext context { get; }

        public IUserRepository UsersRepository
        {
            get => _usersRepository ??= new UserRepository(context);
        }

        public IOrderRepository OrdersRepository
        {
            get => _ordersRepository ??= new OrderRepository(context);
        }

        public IProductRepository ProductsRepository
        {
            get => _productsRepository ??= new ProductRepository(context);
        }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }

        public async Task Rollback()
        {
            await context.DisposeAsync();
        }
    }
}
