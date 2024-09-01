using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Infrastructure.Interfaces
{
    public interface IFileHandler
    {
        Task<string> SaveImage(string contentRootPath, IFormFile imageFile);
    }
}
