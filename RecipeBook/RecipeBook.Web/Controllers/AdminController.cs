using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Enums;
using RecipeBook.Common.Models;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;

        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            return View("List", await userService.GetAllByRoleAsync(Role.Editor));
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!(await userService.DeleteAsync(id)))
            {
                ModelState.AddModelError("", "The user cannot be deleted");
            }

            return await GetAllAsync();
        }

        [HttpGet]
        public IActionResult CreateAsync()
        {
            ViewBag.AdminTitle = "Add";
            return View("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(User user)
        {
            if (await userService.IsUserExist(user.Email))
            {
                ViewBag.AdminTitle = "Add";
                ModelState.AddModelError("", "User with this email already exists");
            }
            else
            {
                user.Role = Role.Editor;
                await userService.CreateAsync(user);

                return await GetAllAsync();
            }
            return View("Edit", user);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            ViewBag.AdminTitle = "Edit";
            return View("Edit", await userService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(User user, int id)
        {
            if (await userService.IsUserExist(user.Email))
            {
                ViewBag.AdminTitle = "Edit";
                ModelState.AddModelError("", "User with this email already exists");
            }
            else
            {
                await userService.UpdateAsync(id, user);

                return await GetAllAsync();
            }
            return View("Edit", user);
        }
    }
}
