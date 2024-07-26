using Juan_PB301EmilMusayev.Models;

namespace Juan_PB301EmilMusayev.ViewModels
{
    public class HomeVM
    {
        public List<Slider>? Sliders { get; set; }
        public List<Product>? Products { get; set; }
        public List<Policy>? Policies { get; set; }
        public string? ProductDescription { get; set; }

    }
}
