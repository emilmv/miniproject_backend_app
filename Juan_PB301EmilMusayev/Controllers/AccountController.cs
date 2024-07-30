using Juan_PB301EmilMusayev.Helpers;
using Juan_PB301EmilMusayev.Interfaces;
using Juan_PB301EmilMusayev.Models;
using Juan_PB301EmilMusayev.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Net;
using System.Net.Mail;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
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
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string link = Url.Action(nameof(VerifyEmail), "Account", new
            {
                email = user.Email,
                token = token
            }, Request.Scheme, Request.Host.ToString());
            string body = string.Empty;
            using (StreamReader streamReader = new("wwwroot/template/VerifyEmailTemplate.html"))
            {
                body = streamReader.ReadToEnd();
            }
            body = body.Replace("{{link}}", link);
            body = body.Replace("{{username}}", user.UserName);
            _emailService.SendEmail(user.Email, "Verify Email", body);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser is null) return NotFound();
            await _userManager.ConfirmEmailAsync(appUser, token);
            await _signInManager.SignInAsync(appUser, false);
            return RedirectToAction("index", "home");
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
            if (User.Identity.IsAuthenticated) return RedirectToAction("index", "home");
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
                if (user is null)
                {
                    ModelState.AddModelError("", "Username or password is not correct");
                    return View(loginVM);
                }
            }
            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Email Confirmation needed");
                return View(loginVM);
            }
            if (isCorrectPassword)
            {
                if (user.IsBlocked)
                {
                    ModelState.AddModelError("", "Account is blocked");
                    return View(loginVM);
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.Remember, true);

            //SignInResult result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.Remember, true);

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
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser is null)
            {
                ModelState.AddModelError("email", "Email has not been registered before");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            string url = Url.Action(nameof(ResetPassword), "Account", new
            {
                email = appUser.Email,
                token
            }, Request.Scheme, Request.Host.ToString());
            string body = string.Empty;
            using (StreamReader streamReader = new("wwwroot/template/ResetPasswordTemplate.html"))
            {
                body = streamReader.ReadToEnd();
            }
            body = body.Replace("{{link}}", url);
            body = body.Replace("{{username}}", appUser.UserName);
            _emailService.SendEmail(appUser.Email, "Reset Password", body);
            return View();
        }
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var user=await _userManager.FindByEmailAsync(email);
            if (user is null) return NotFound();
            bool result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            if (!result) return Content("Token Expired");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string token, ResetPaswordVM resetPasswordVM)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (ModelState.IsValid) return View();
            var result = await _userManager.ResetPasswordAsync(appUser, token, resetPasswordVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(resetPasswordVM);
            }
            return RedirectToAction("index","home");
        }
    }
}
