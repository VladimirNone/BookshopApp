using System;

namespace BookshopApp.Models
{
    public class OrderedProduct : Entity
    {
        public DateTime TimeOfBuing { get; set; }
        public int Count { get; set; }
        public bool Cancelled { get; set; } 
        public string CurrentLocation { get; set; }
        public string ReasonForCancellation { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
