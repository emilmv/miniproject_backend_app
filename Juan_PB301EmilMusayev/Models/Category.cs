namespace Juan_PB301EmilMusayev.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> ChildCategories { get; set; }
        public List<Product> Products { get; set; }
        public bool IsMainCategory { get; set; }
    }
}
