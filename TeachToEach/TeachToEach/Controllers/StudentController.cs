using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TeachToEach.Domain.ViewModels.Student;
using TeachToEach.Domain.ViewModels.Teacher;
using TeachToEach.Service.Implementations;
using TeachToEach.Service.Interfaces;

namespace TeachToEach.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Student()
        {
            var response = await _studentService.GetStudentByLogin(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }
        public async Task<IActionResult> GetTeachers()
        {
            var response = await _studentService.GetTeachers(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> GetHWtoStudent()
        {
            var response = await _studentService.GetHomewoks(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> FindTeacher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FindTeacher(TeacherFindViewModel teacherFindViewModel)
        {
            
            var response = await _studentService.FindTeacher(teacherFindViewModel);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("ShowFoundTeachers", response.Data);
            }
            return RedirectToAction("Error");
        }


        public async Task<IActionResult> ShowFoundTeachers(IEnumerable<TeacherProfileViewModel> teachers)
        {
            return View(teachers);
        } 

        public async Task<IActionResult> CreaetRequest(string teacher_login, string subject)
        {
            var response = await _studentService.CreateRequest(User.Identity.Name, teacher_login, subject);
            
            return RedirectToAction("GetTeachers");
        }
    }
}
