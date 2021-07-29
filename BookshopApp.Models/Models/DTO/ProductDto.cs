using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Models.DTO
{
    public class ProductDto : Entity
    {
        public string Name { get; set; }
        public short YearOfRelease { get; set; }
        public string Description { get; set; }
        public string LinkToImage { get; set; }
        public double Price { get; set; }
        public double CountInStock { get; set; }
        public DateTime DateOfPublication { get; set; }
        public AuthorDto Author { get; set; }
    }
}
