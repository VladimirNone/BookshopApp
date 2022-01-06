using BookshopApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Infrastructure.Implementations
{
    public class FileHandler : IFileHandler
    {
        public async Task<string> SaveImage(string contentRootPath, IFormFile imageFile)
        {
            var linkToImage = Path.Combine("Images", imageFile.FileName);
            var path = Path.Combine(contentRootPath, "ClientApp", "public", linkToImage);

            using var fileStream = new FileStream(path, FileMode.Create);
            await imageFile.CopyToAsync(fileStream);

            return linkToImage;
        }
    }
}
