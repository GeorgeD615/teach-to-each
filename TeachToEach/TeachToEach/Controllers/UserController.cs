using Microsoft.AspNetCore.Mvc;
using TeachToEach.DAL.Interfaces;
using TeachToEach.Domain.Entity;

namespace TeachToEach.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await userRepository.Select();
            return View(response);
        }

        public async Task<IActionResult> Teacher()
        {
            //var response = await userRepository.Select();
            return View();
        }



    }
}
