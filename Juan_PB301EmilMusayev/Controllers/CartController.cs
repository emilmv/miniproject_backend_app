using Juan_PB301EmilMusayev.Data;
using Juan_PB301EmilMusayev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class CartController : Controller
    {
        private readonly JuanDbContext _context;

        public CartController(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            string cart = HttpContext.Request.Cookies["cart"];
            List<CartVM> carts;
            if (string.IsNullOrWhiteSpace(cart))
            {
                carts = new();
            }
            else
            {
                carts =JsonConvert.DeserializeObject<List<CartVM>>(cart);
            }
            if(carts.Exists(p=>p.Id == id))
            {
                var cartProduct=carts.FirstOrDefault(p=>p.Id == id);
                cartProduct.Count++;
            }
            else
            {
                carts.Add(new CartVM()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = (double)product.DiscountPrice,
                    Image = product.DisplayImage,
                    Count = 1
                });
            }
            HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(carts));
            return PartialView("_CartPartial",carts);
        }
        public IActionResult CartItems()
        {
            return View();
        }
    }
}
