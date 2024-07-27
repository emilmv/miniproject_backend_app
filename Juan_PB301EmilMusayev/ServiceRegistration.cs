using Juan_PB301EmilMusayev.Data;
using Juan_PB301EmilMusayev.Interfaces;
using Juan_PB301EmilMusayev.Services;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddDbContext<JuanDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(30);
            });
            services.AddHttpContextAccessor();
        }
    }
}