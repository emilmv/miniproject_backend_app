using Juan_PB301EmilMusayev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.ViewComponents
{
    public class PolicyViewComponent:ViewComponent
    {

        private readonly JuanDbContext _context;

        public PolicyViewComponent(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var policies = await _context.Policies
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            return View(await Task.FromResult(policies));
        }
    }
}
