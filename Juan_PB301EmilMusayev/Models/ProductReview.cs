namespace Juan_PB301EmilMusayev.Models
{
    public class ProductReview:BaseEntity
    {
        public string? Description { get; set; }
        public int ProductId { get; set; }
        public Product? product { get; set; }
    }
}
