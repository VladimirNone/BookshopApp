using System;

namespace BookshopApp.Models
{
    public class Product : Entity<Product>
    {
        public string Name { get; set; }
        public short YearOfRelease { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public double Price { get; set; }
        public double CountInStock { get; set; }
    }
}
