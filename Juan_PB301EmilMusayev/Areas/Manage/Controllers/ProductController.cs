using Juan_PB301EmilMusayev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private readonly JuanDbContext _context;

        public ProductController(JuanDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products= _context.Products
                .AsNoTracking()
                .Where(p=>!p.IsDeleted)
                .Include(p=>p.Category)
                .ToList();
            return View(products);
        }
    }
}
