using Identity.CoreWebUI.Context;
using Identity.CoreWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;

namespace Identity.CoreWebUI.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;


        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult AddRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddRole(ViewModelAppRole model)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole()
                {
                    Name = model.Name
                };

                var addedRoleResult = await _roleManager.CreateAsync(role);

                if (addedRoleResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in addedRoleResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }



        public IActionResult UpdateRole(int id)
        {
            AppRole findRole = _roleManager.Roles.FirstOrDefault(x => x.Id == id);

            if (findRole == null)
            {

            }

            ViewModelAppRole model = new ViewModelAppRole();
            model.Name = findRole.Name;
            model.Id = findRole.Id;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateRole(ViewModelAppRole model)
        {
            if (ModelState.IsValid)
            {
                AppRole findRole = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Id);

                findRole.Name = model.Name;
                findRole.Id = model.Id;

                var updatedRoleResult = await _roleManager.UpdateAsync(findRole);

                if (updatedRoleResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in updatedRoleResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteRole(ViewModelAppRole model)
        {
            AppRole findRole = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Id);


            var deletedRoleResult = await _roleManager.DeleteAsync(findRole);

            if (deletedRoleResult.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var item in deletedRoleResult.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }


        public IActionResult UserList()
        {
            return View(_userManager.Users.ToList());
        }

        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            TempData["UserId"] = user.Id;

            var roles = _roleManager.Roles.ToList();

            var userRoles = await _userManager.GetRolesAsync(user);

            List<ViewModelAssignRole> models = new List<ViewModelAssignRole>();

            foreach (var role in roles)
            {
                ViewModelAssignRole model = new ViewModelAssignRole();
                model.RoleId = role.Id;
                model.Name = role.Name;
                model.Exists = userRoles.Contains(role.Name);
                models.Add(model);

            }

            return View(models);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(List<ViewModelAssignRole> modelList)
        {

            int userId = (int)TempData["UserId"];

            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            foreach (var item in modelList)
            {
                if (item.Exists)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);

                }
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> AssignClaim(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            TempData["UserId"] = user.Id;


            var userClaims = await _userManager.GetClaimsAsync(user);

            if (userClaims.Count <= 0)
            {
                Claim claim = new Claim("gender", "female");
                await _userManager.AddClaimAsync(user, claim);
            }

            return RedirectToAction("UserList", "Role");
        }


    }
}
