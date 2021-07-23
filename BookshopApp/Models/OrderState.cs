
using System.Collections.Generic;

namespace BookshopApp.Models
{
    public class OrderState : Entity
    {
        public string NameOfState { get; set; }

        public List<Order> Orders { get; set; }
    }
}
