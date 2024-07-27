using Juan_PB301EmilMusayev.Services;
using Microsoft.AspNetCore.Mvc;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly LayoutService _layoutService;

        public ContactUsController(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public IActionResult Index()
        {
            return View(_layoutService.GetSettings());
        }
    }
}
