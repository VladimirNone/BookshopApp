using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Models.DTO
{
    public class CartDto
    {
        public string FinalLocation { get; set; }
        public string ReasonForCancellation { get; set; }

        public double FinalAmount { get; set; }

        public List<OrderedProduct> OrderedProducts { get; set; }
    }
}
