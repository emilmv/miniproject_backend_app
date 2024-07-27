using Juan_PB301EmilMusayev.Data;
using Juan_PB301EmilMusayev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class ShopController : Controller
    {
        private readonly JuanDbContext _context;

        public ShopController(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ShopVM shopVM = new ShopVM();
            shopVM.Products = await _context.Products
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .ToListAsync();
            shopVM.Colors = await _context.Colors
                .AsNoTracking()
                .Include(c=>c.ProductColors)
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            shopVM.Sizes = await _context.Sizes
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            shopVM.Categories = await _context.Categories
                .ToListAsync();
            return View(shopVM);
        }
    }
}
