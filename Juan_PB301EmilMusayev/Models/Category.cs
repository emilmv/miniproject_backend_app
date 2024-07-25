using System.ComponentModel.DataAnnotations;

namespace Juan_PB301EmilMusayev.Models
{
    public class Category:BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public List<Category>? Children { get; set; }
        public bool IsMainCategory { get; set; }
    }
}
