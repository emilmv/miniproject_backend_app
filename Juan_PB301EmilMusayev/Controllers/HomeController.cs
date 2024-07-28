using Juan_PB301EmilMusayev.Data;
using Juan_PB301EmilMusayev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class HomeController : Controller
    {
        private readonly JuanDbContext _context;
        public HomeController(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new();
            homeVM.ProductDescription = _context.Settings.FirstOrDefault(s => s.Key == "ProductDescription").Value;
            homeVM.Products = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.ProductImages)
                .Include(p => p.Ratings)
                .Take(4)
                .ToListAsync();
            homeVM.Banners = await _context.Banners
                .Where(b => !b.IsDeleted)
                .ToListAsync();
            homeVM.BlogDescription = _context.Settings.FirstOrDefault(s => s.Key == "OurBlog").Value;
            homeVM.Brands = await _context.Brands
                .Where(b => !b.IsDeleted)
                .ToListAsync();
            return View(homeVM);
        }
    }
}
