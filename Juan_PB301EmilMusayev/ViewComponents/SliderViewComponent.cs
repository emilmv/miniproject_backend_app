using Juan_PB301EmilMusayev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly JuanDbContext _context;

        public SliderViewComponent(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _context.Sliders
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            return View(await Task.FromResult(sliders));
        }
    }
}
