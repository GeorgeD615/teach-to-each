using Microsoft.AspNetCore.Mvc;

namespace TeachToEach.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Student()
        {
            return View();
        }
        public async Task<IActionResult> GetTeachers()
        {
            return View();
        }

        public async Task<IActionResult> GetHWtoStudent()
        {
            return View();
        }
    }
}
