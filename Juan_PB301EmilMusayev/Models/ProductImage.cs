﻿namespace Juan_PB301EmilMusayev.Models
{
    public class ProductImage:BaseEntity
    {
        public string? Image { get; set; }
        public int ProductId { get; set; }
        public Product? product { get; set; }
    }
}
