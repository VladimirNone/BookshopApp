using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Models
{
    public enum OrderStateEnum
    {
        IsCart = 1,
        Confirmed,
        Completed,
        Cancelled
    }
}
