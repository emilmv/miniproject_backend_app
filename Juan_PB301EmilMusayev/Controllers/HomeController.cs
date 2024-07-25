using Microsoft.AspNetCore.Mvc;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
