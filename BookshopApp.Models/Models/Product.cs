using System;
using System.Collections.Generic;

namespace BookshopApp.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public short YearOfRelease { get; set; }
        public string Description { get; set; }
        public string LinkToImage { get; set; }
        public double Price { get; set; }
        public double CountInStock { get; set; }
        public DateTime DateOfPublication { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<OrderedProduct> OrderedProducts { get; set; }
    }
}
