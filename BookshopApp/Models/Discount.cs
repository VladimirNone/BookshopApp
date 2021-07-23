using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Models
{
    public class Discount: Entity
    {
        public double Percent { get; set; }
        public int NumberOfUses { get; set; }

        public List<User> Owners { get; set; }
    }
}
