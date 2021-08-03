using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Models.DTO
{
    /// <summary>
    /// User private info for admins and owner
    /// </summary>
    public class UserPrivateDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
