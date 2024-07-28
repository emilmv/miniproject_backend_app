using Juan_PB301EmilMusayev.Models;
using Juan_PB301EmilMusayev.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            AppUser user = new()
            {
                UserName = registerVM.Username,
                Email = registerVM.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }
            await _userManager.AddToRoleAsync(user, "member"); 
            return RedirectToAction("Index", "Home");
        }
        //public async Task<IActionResult> RoleCreation()
        //{
        //    if (!await _roleManager.RoleExistsAsync("admin")) await _roleManager.CreateAsync(new(){Name = "admin"});
        //    if (!await _roleManager.RoleExistsAsync("admin")) await _roleManager.CreateAsync(new(){Name = "member"});
        //    if (!await _roleManager.RoleExistsAsync("admin")) await _roleManager.CreateAsync(new(){Name = "superadmin"});
        //    return Content("Added");
        //}
    }
}
