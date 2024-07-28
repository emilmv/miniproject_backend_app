using Juan_PB301EmilMusayev.Models;
using Juan_PB301EmilMusayev.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            AppUser user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            if(user is null)
            {
                user= await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            }
            if(user is null)
            {
                ModelState.AddModelError("", "Username or password is not correct");
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.Remember, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Too many attempts, please try again later");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is not correct");
                return View(loginVM);
            }
            return View();
        }
    }
}
