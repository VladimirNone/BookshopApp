using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Models.DTO
{
    public class CartProductDto
    {
        public int Count { get; set; }

        public ProductDto Product { get; set; }
    }
}
