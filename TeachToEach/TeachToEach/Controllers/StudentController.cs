using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.ViewModels;
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
            if (ModelState.IsValid)
            {
                var response = await _studentService.FindTeacher(teacherFindViewModel);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View("ShowFoundTeachers", response.Data);
                }
            } 
            return View(teacherFindViewModel);
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

        public async Task<IActionResult> UpdateHomeworkFirstStep(int id, string solution, string description, string teacherComment)
        {
            return RedirectToAction("UpdateHomeworkPage", new { id = id, solution = solution, description = description, teacherComment = teacherComment });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHomeworkPage(int id, string solution, string description, string teacherComment)
        {
            HomeworkUpdateViewModel updateViewModel = new HomeworkUpdateViewModel()
            {
                id = id,
                solution = solution,
                description = description,
                teacher_comment = teacherComment
            };
            return View(updateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHomeworkPage(HomeworkUpdateViewModel updateViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _studentService.UpdateHomework(updateViewModel);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetHWtoStudent");
                }
            }
            return View(updateViewModel);
        }

        public async Task<IActionResult> CreateRatingFirstStep(int relation_id)
        {
            return RedirectToAction("CreateRating", new { relation_id = relation_id });
        }

        [HttpGet]
        public async Task<IActionResult> CreateRating(int relation_id)
        {
            CreateRatingViewModel model = new CreateRatingViewModel()
            {
                relation_id = relation_id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating(CreateRatingViewModel createRatingViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _studentService.CreateRating(createRatingViewModel);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetTeachers");
                }
            }
            return View(createRatingViewModel);
        }
    }
}
