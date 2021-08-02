using BookshopApp.Db;
using BookshopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Db.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T: class, IEntity
    {
        public IUnitOfWork UnitOfWork { get; protected set; }
        public DbSet<T> DbSet { get; protected set; }
        protected ApplicationDbContext AppDbContext { get; private set; }

        public Repository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            AppDbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public virtual async Task AddEntityAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            await DbSet.AddAsync(entity);
        }

        public virtual async Task AddEntitiesAsync(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            await DbSet.AddRangeAsync(entities);
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbSet.Update(entity);
        }

        public virtual async Task<T> GetEntityAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
