using Juan_PB301EmilMusayev.Models;

namespace Juan_PB301EmilMusayev.ViewModels
{
    public class ShopVM
    {
        public List<Product> Products { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Category> Categories { get; set; }
    }
}
