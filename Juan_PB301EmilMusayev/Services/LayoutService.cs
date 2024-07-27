using Juan_PB301EmilMusayev.Data;
using Juan_PB301EmilMusayev.Models;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Services
{
    public class LayoutService
    {
        private readonly JuanDbContext _context;

        public LayoutService(JuanDbContext context)
        {
            _context = context;
        }
        public IDictionary<string, string> GetSettings() => _context.Settings
            .Where(s => !s.IsDeleted)
            .ToDictionary(s => s.Key, s => s.Value);
    }
}
