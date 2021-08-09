using BookshopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Db
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<List<Author>> GetAuthorsNoTracked();
    }
}
