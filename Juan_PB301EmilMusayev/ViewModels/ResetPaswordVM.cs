using System.ComponentModel.DataAnnotations;

namespace Juan_PB301EmilMusayev.ViewModels
{
    public class ResetPaswordVM
    {
        [Required,MaxLength(32),DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(32), DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

    }
}
