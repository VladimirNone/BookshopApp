using BookshopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public interface IRepository<T> where T : class, IEntity
    {
        IUnitOfWork UnitOfWork { get; }
        DbSet<T> DbSet { get; }

        Task AddEntityAsync(T entity);
        void Update(T entity);
        Task<T> GetEntityAsync(int id);
    }
}
