using Juan_PB301EmilMusayev.Models;

namespace Juan_PB301EmilMusayev.ViewModels
{
    public class HomeVM
    {
        public List<Product> Products { get; set; }
        public List<Banner> Banners { get; set; }
        public List<Brand> Brands { get; set; }
        public string ProductDescription { get; set; }
        public string BlogDescription { get; set; }
    }
}
