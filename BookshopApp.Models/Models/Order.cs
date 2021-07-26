using System;
using System.Collections.Generic;

namespace BookshopApp.Models
{
    public class Order : Entity
    {
        public string FinalLocation { get; set; }
        public string ReasonForCancellation { get; set; }

        public DateTime DateOfOrdering { get; set; }
        public DateTime DateOfPaing { get; set; }
        public DateTime DateOfClosing { get; set; }

        public string CustomerId { get; set; }
        public User Customer { get; set; }

        public int StateId { get; set; }
        public OrderState State { get; set; }

        public List<OrderedProduct> OrderedProducts { get; set; }
    }
}
