using Juan_PB301EmilMusayev.Models;
using Microsoft.AspNetCore.Identity;

namespace Juan_PB301EmilMusayev.ViewModels
{
    public class UpdateRoleVM
    {
        public  AppUser User { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
