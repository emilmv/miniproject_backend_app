namespace Juan_PB301EmilMusayev.Models
{
    public class ProductSize:BaseEntity
    {
        public int ProductId { get; set; }
        public Product product { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
    }
}
