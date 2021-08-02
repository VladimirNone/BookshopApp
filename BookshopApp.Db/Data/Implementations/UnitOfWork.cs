using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Db.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _usersRepository;
        private IOrderRepository _ordersRepository;
        private IProductRepository _productsRepository;
        private ApplicationDbContext _context { get; }
        private ILogger<UnitOfWork> _logger { get; }

        public IUserRepository UsersRepository
        {
            get => _usersRepository ??= new UserRepository(_context, this);
        }

        public IOrderRepository OrdersRepository
        {
            get => _ordersRepository ??= new OrderRepository(_context, this);
        }

        public IProductRepository ProductsRepository
        {
            get => _productsRepository ??= new ProductRepository(_context, this);
        }

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Commit()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _context.DisposeAsync();
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task Rollback()
        {
            await _context.DisposeAsync();
        }
    }
}
