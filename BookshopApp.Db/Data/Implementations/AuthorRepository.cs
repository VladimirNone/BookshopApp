
using BookshopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Db.Implementations
{
    class AuthorRepository: Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {

        }

        public async Task<List<Author>> GetAuthors()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }
    }
}
