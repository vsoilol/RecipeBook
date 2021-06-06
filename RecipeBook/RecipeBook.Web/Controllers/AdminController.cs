using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Enums;
using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View("Index", await userService.GetAllAsync());
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await userService.DeleteAsync(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ошибка");
            }

            return await GetAllAsync();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(User user)
        {
            user.Role = Role.Editor;
            await userService.CreateAsync(user);
            return await GetAllAsync();
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await userService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            await userService.UpdateAsync(user.Id, user);

            return await GetAllAsync();
        }
    }
}
