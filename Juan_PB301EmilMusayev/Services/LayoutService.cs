using Juan_PB301EmilMusayev.Data;
using Juan_PB301EmilMusayev.Interfaces;
using Juan_PB301EmilMusayev.Models;
using Juan_PB301EmilMusayev.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Juan_PB301EmilMusayev.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly JuanDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public LayoutService(JuanDbContext context,IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _context.Products
            .AsNoTracking()
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        public IDictionary<string, string> GetSettings() => _context.Settings
            .AsNoTracking()
            .Where(s => !s.IsDeleted)
            .ToDictionary(s => s.Key, s => s.Value);

        public IEnumerable<CartVM> GetCart()
        {
            List<CartVM> list = new();
            string cart = _contextAccessor.HttpContext.Request.Cookies["cart"];
            if (string.IsNullOrWhiteSpace(cart)) return list;
            else
            {
                list=JsonConvert.DeserializeObject<List<CartVM>>(cart);
                foreach(var cartProduct in list)
                {
                    var existProduct= _context.Products.AsNoTracking().FirstOrDefault(p=>p.Id==cartProduct.Id);
                    cartProduct.Name = existProduct.Name;
                    cartProduct.Price = (double)existProduct.DiscountPrice > 0 ? (double)existProduct.DiscountPrice : (double)existProduct.SalePrice;
                    cartProduct.Image=existProduct.DisplayImage;
                }
                return list;
            }
        }
    }
}
