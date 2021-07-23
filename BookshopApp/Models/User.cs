using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Models
{
    public class User : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public bool UserDeleted { get; set; }

        public int DiscountId { get; set; }
        public Discount Discount { get; set; }

        public List<Order> UserOrders { get; set; }
    }
}
