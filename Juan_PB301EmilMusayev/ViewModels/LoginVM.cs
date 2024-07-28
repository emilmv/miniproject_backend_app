using System.ComponentModel.DataAnnotations;

namespace Juan_PB301EmilMusayev.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }


        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember me?")]
        public bool Remember { get; set; }
    }
}
