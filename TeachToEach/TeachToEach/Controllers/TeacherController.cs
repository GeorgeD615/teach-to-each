using Microsoft.AspNetCore.Mvc;

namespace TeachToEach.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Teacher()
        {
            return View();
        }

        public async Task<IActionResult> GetStudents()
        {
            return View();
        }

        public async Task<IActionResult> GetHWtoTeacher()
        {
            return View();
        }

    }
}
