using System.Collections.Generic;

namespace BookshopApp.Models
{
    public class Author : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Product> ListOfWorks { get; set; }
    }
}