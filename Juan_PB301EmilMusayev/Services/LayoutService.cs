using Juan_PB301EmilMusayev.Data;
using Juan_PB301EmilMusayev.Interfaces;
using Juan_PB301EmilMusayev.Models;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly JuanDbContext _context;

        public LayoutService(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts() => await _context.Products
            .AsNoTracking()
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        public IDictionary<string, string> GetSettings() => _context.Settings
            .AsNoTracking()
            .Where(s => !s.IsDeleted)
            .ToDictionary(s => s.Key, s => s.Value);
    }
}
