using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachToEach.DAL.Interfaces;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.ViewModels.User;
using TeachToEach.Models;
using TeachToEach.Service.Interfaces;

namespace TeachToEach.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await userService.GetUsers();

            if(response.Data != null)
                return View(response);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var response = await userService.GetUser(id);

            if (response.Data != null)
                return View(response);
            return RedirectToAction("Error");
        }


        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await userService.DeleteUser(id);

            if (response.Data)
                return RedirectToAction("GetUsers");
            return RedirectToAction("Error");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await userService.CreateUser(userViewModel);
            }
            return RedirectToAction("GetUsers");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(UpdateUserViewModel updateUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await userService.EditUser(updateUserViewModel.id, updateUserViewModel.userViewModel);
            }
            return RedirectToAction("GetUsers");
        }




    }
}
