using Juan_PB301EmilMusayev.Models;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Data
{
    public class JuanDbContext : DbContext
    {
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Banner> Banners { get; set; }

        public JuanDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
