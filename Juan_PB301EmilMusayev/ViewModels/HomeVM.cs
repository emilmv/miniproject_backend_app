using Azure.Core.Pipeline;
using Juan_PB301EmilMusayev.Models;

namespace Juan_PB301EmilMusayev.ViewModels
{
    public class HomeVM
    {
        public List<Slider>? Sliders { get; set; }
        public List<Product>? Products { get; set; }
        public List<Policy>? Policies { get; set; }
        public List<Banner>? Banners { get; set; }
        public List<Blog>? Blogs { get; set; }
        public List<Brand>? Brands { get; set; }
        public string? ProductDescription { get; set; }
        public string? BlogDescription { get; set; }

    }
}
