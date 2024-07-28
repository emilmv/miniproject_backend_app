using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juan_PB301EmilMusayev.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Precision(18, 2)]
        public decimal SalePrice { get; set; }
        [Precision(18, 2)]
        public decimal DiscountPrice { get; set; }
        public int TaxPercentage { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public List<ProductSize> ProductSizes{ get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<ProductReview> ProductReviews { get; set; }
        public string DisplayImage { get; set; }
        public bool IsNewArrival { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [NotMapped]
        public IFormFile MainPhoto { get; set; }
        [NotMapped]
        public IFormFile[] Photos { get;set; }
    }
}
