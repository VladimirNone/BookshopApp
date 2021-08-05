using BookshopApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Models.DTO
{
    public class ProductInputDto
    {
        public string Name { get; set; }
        public short YearOfRelease { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public double Price { get; set; }
        public double CountInStock { get; set; }
        public int AuthorId { get; set; }
    }
}
