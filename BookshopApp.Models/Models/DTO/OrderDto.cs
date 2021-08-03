using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Models.DTO
{
    public class OrderDto : Entity
    {
        public string FinalLocation { get; set; }
        public string ReasonForCancellation { get; set; }

        public double FinalAmount { get; set; }

        public DateTime DateOfOrdering { get; set; }
        public DateTime DateOfClosing { get; set; }

        public UserPrivateDto Customer { get; set; }

        public OrderState State { get; set; }

        public List<CartProductDto> OrderedProducts { get; set; }
    }
}
