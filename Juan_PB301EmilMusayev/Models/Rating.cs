namespace Juan_PB301EmilMusayev.Models
{
    public class Rating:BaseEntity
    {
        public int StarCount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
