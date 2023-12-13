using Microsoft.AspNetCore.Mvc;
using TeachToEach.Service.Interfaces;

namespace TeachToEach.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Teacher()
        {
            var response = await _teacherService.GetTeacherByLogin(User.Identity.Name);
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> GetStudents()
        {
            var response = await _teacherService.GetStudents(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> GetHWtoTeacher()
        {
            var response = await _teacherService.GetHomewoks(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

    }
}
