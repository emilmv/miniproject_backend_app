using Juan_PB301EmilMusayev.Data;
using Juan_PB301EmilMusayev.Interfaces;
using Juan_PB301EmilMusayev.Models;
using Juan_PB301EmilMusayev.Services;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IEmailService, EmailService>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(30);
            });
            services.AddHttpContextAccessor();
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase=true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail=true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<JuanDbContext>().AddDefaultTokenProviders();
        }
    }
}