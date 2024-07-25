using Juan_PB301EmilMusayev.Models;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Data
{
    public class JuanDbContext : DbContext
    {
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public JuanDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
