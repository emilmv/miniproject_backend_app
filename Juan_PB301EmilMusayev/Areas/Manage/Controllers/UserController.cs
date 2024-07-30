using Juan_PB301EmilMusayev.Helpers;
using Juan_PB301EmilMusayev.Models;
using Juan_PB301EmilMusayev.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            var users = searchText is null ? await _userManager.Users.ToListAsync() : await _userManager.Users.Where(u => u.UserName.ToLower().Contains(searchText.ToLower())).ToListAsync();
            List<UserRoleVM> list = new();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                list.Add(new() { User = user, UserRoles = userRoles });
            }
            return View(list);
        }
        public async Task<IActionResult> ChangeUserStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return BadRequest();
            user.IsBlocked = !user.IsBlocked;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateUserRole(string id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            UpdateRoleVM updateRoleVM = new();
            updateRoleVM.User = user;
            updateRoleVM.Roles = roles;
            updateRoleVM.UserRoles = userRoles;
            return View(updateRoleVM);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string id, List<string> newRoles)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();

            if (newRoles.Count == 0)
            {
                UpdateRoleVM updateRoleVM = new();
                updateRoleVM.User = user;
                updateRoleVM.Roles = roles;
                updateRoleVM.UserRoles = userRoles;
                ModelState.AddModelError("roles", "Please choose at lease one role");
                return View(updateRoleVM);
            }
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            await _userManager.AddToRolesAsync(user, newRoles);
            return RedirectToAction("Index");
        }
    }
}
