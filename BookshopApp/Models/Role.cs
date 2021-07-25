using Microsoft.AspNetCore.Identity;


namespace BookshopApp.Models
{
    public class Role : IdentityRole<int>, IEntity
    {
        public Role(string name)
            :base(name)
        {

        }
    }
}
