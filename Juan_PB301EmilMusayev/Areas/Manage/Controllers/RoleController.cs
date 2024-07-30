using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan_PB301EmilMusayev.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("role", "role field can not be empty");
                return View();
            }
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
            else
            {
                ModelState.AddModelError("role", "Role already exists");
                return View();
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles ="superadmin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Update(string id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id,string roleName)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("role", "role field can not be empty");
                return View();
            }
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                ModelState.AddModelError("role", "Role already exists");
                return View();
            }
            role.Name = roleName;
            await _roleManager.UpdateAsync(role);
            return RedirectToAction("index");
            
        }
    }
}
