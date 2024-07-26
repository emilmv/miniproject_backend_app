using Juan_PB301EmilMusayev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class ProductController : Controller
    {
        private readonly JuanDbContext _context;

        public ProductController(JuanDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ProductModal(int? id)
        {
            if (id == null) return BadRequest();
            var product= await _context.Products.AsNoTracking()
                .Where(p=>!p.IsDeleted)
                .Include(p=>p.ProductImages)
                .Include(p=>p.ProductReviews)
                .FirstOrDefaultAsync(p=>p.Id == id);
            if (product == null) return NotFound();
            return PartialView("_ModalPartial",product);
        }
    }
}
