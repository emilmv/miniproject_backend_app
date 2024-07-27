using Juan_PB301EmilMusayev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.ViewComponents
{
    public class BlogViewComponent:ViewComponent
    {
        private readonly JuanDbContext _context;

        public BlogViewComponent(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _context.Blogs
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            return View(await Task.FromResult(blogs));
        }
    }
}
