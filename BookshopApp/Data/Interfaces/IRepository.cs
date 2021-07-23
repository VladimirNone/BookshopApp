using BookshopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Data.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        DbSet<T> DbSet { get; }

        Task AddEntityAsync(T entity);
        void Update(T entity);
        Task<T> GetEntityAsync(int id);
    }
}
