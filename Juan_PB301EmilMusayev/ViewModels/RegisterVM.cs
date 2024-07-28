using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Juan_PB301EmilMusayev.ViewModels
{
    public class RegisterVM
    {
        [Required,MinLength(4)]
        public string Username { get; set; }


        [Required,EmailAddress,DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required,DataType(DataType.Password)]
        public string Password { get; set; }


        [Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string RepeatedPassword { get; set; }
    }
}
