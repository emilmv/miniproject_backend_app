using Juan_PB301EmilMusayev.Data;
using Microsoft.AspNetCore.Mvc;

namespace Juan_PB301EmilMusayev.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly JuanDbContext _context;

        public FooterViewComponent(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(IDictionary<string,string>settings)
        {
            return View(await Task.FromResult(settings));
        }
    }
}
