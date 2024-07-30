using Juan_PB301EmilMusayev.Models;

namespace Juan_PB301EmilMusayev.ViewModels
{
    public class UserRoleVM
    {
        public AppUser User { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
