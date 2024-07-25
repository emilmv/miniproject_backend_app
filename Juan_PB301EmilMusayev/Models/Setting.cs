using System.ComponentModel.DataAnnotations;

namespace Juan_PB301EmilMusayev.Models
{
    public class Setting : BaseEntity
    {
        [Required]
        public string? Key { get; set; }
        [MaxLength(1500)]
        public string? Value { get; set; }
    }
}
