using Juan_PB301EmilMusayev.Helpers;
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
            await _userManager.AddToRoleAsync(user, UserRoles.member.ToString());
            return RedirectToAction("Index", "Home");
        }
        //public async Task<IActionResult> RoleCreation()
        //{
        //    if (!await _roleManager.RoleExistsAsync(UserRoles.admin.ToString())) await _roleManager.CreateAsync(new() { Name = UserRoles.admin.ToString() });
        //    if (!await _roleManager.RoleExistsAsync(UserRoles.member.ToString())) await _roleManager.CreateAsync(new() { Name = UserRoles.member.ToString() });
        //    if (!await _roleManager.RoleExistsAsync(UserRoles.superadmin.ToString())) await _roleManager.CreateAsync(new() { Name = UserRoles.superadmin.ToString() });
        //    return Content("Added");
        //}

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string returnURL)
        {
            if (!ModelState.IsValid) return View(loginVM);

            AppUser user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);

            if (user is null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            }
            if (user is null)
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
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains(UserRoles.admin.ToString())) return RedirectToAction("index", "dashboard", new { area = "manage" });
            if (roles.Contains(UserRoles.superadmin.ToString())) return RedirectToAction("index", "dashboard", new { area = "manage" });
            if (returnURL is null)
            {
                return RedirectToAction("index", "home");
            }
            return Redirect(returnURL);


        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }




    }
}
