using Microsoft.AspNetCore.Identity;

namespace Juan_PB301EmilMusayev.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsBlocked { get; set; }


    }
}
