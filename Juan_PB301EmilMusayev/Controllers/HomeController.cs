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
            homeVM.Sliders= await _context.Sliders.Where(s => !s.IsDeleted).ToListAsync();
            homeVM.Policies = await _context.Policies.Where(s => !s.IsDeleted).ToListAsync();
            homeVM.ProductDescription = _context.Settings.Where(s => s.Key == "ProductDescription"&&!s.IsDeleted).FirstOrDefault().Value.ToString();
            homeVM.Products = await _context.Products.Include(p=>p.ProductImages).ToListAsync();
       
            return View(homeVM);
        }
    }
}
