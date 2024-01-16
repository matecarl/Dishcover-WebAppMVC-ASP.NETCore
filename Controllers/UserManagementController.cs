using Dishcover.Areas.Identity.Data;
using Dishcover.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dishcover.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class UserManagementController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(ApplicationDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: UserManagementController
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var userManagementViewModels = new List<ManageUserViewModel>();
            foreach (var user in users)
            {
                userManagementViewModels.Add(
                    new ManageUserViewModel()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        PhoneNumber = user.PhoneNumber,
                        LockoutEnd = user.LockoutEnd,
                        LockoutEnabled = user.LockoutEnabled,
                        AccessFailedCount = user.AccessFailedCount,
                        Roles = await _userManager.GetRolesAsync(user) //.Result
                    }
                );
            }

            return View(userManagementViewModels);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRoles(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.UserName = user.UserName;

            var userRoles = new List<UserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRole = new UserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }
                userRoles.Add(userRole);
            }
            return View(userRoles);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoles(List<UserRolesViewModel> Model, string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            if (Model.Where(r => r.IsSelected).Count() == 0)
            {
                ModelState.AddModelError("", "Please select at least one role.");
                return View(Model);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                return View(Model);
            }

            result = await _userManager.AddToRolesAsync(user, Model.Where(r => r.IsSelected).Select(r => r.RoleName));
            if (!result.Succeeded)
            {
                return View(Model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
