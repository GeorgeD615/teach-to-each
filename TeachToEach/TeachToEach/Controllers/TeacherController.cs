using Microsoft.AspNetCore.Mvc;
using TeachToEach.Domain.ViewModels;
using TeachToEach.Domain.ViewModels.Teacher;
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

        public async Task<IActionResult> ResponseRequest(int relation_id, int status_id)
        {
            var response = await _teacherService.ResponseRequest(relation_id, status_id);
            return RedirectToAction("GetStudents");
        }


        public async Task<IActionResult> CreateHomeworkFirstStep(int relation_id)
        {
            return RedirectToAction("CreateHomeworkPage", new { relation_id = relation_id });
        }

        [HttpGet]
        public async Task<IActionResult> CreateHomeworkPage(int relation_id) {
            HomeworkCreateViewModel homeworkCreateViewModel = new HomeworkCreateViewModel() { relation_id = relation_id};
            return View(homeworkCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHomeworkPage(HomeworkCreateViewModel homeworkCreateViewModel)
        {
            var response = await _teacherService.CreateHomework(homeworkCreateViewModel);
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetHWtoTeacher");
            }
            return RedirectToAction("GetHWtoTeacher");
        }

        public async Task<IActionResult> UpdateHomeworkFirstStep(int id, string description, DateTime? deadline, string teacherComment, bool isCompleted, string solution)
        {
            return RedirectToAction("UpdateHomeworkPage", new { id = id, description = description, deadline = deadline, teacherComment = teacherComment, isCompleted = isCompleted, solution = solution});
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHomeworkPage(int id, string description, DateTime? deadline, string teacherComment, bool isCompleted, string solution)
        {
            HomeworkUpdateViewModel updateViewModel = new HomeworkUpdateViewModel()
            {
                id = id,
                description = description,
                deadline = deadline,
                teacher_comment = teacherComment,
                is_completed = isCompleted,
                solution = solution
            };
            return View(updateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHomeworkPage(HomeworkUpdateViewModel updateViewModel)
        {
            var response = await _teacherService.UpdateHomework(updateViewModel);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetHWtoTeacher");
            }
            return RedirectToAction("GetHWtoTeacher");
        }

        public async Task<IActionResult> GetRatings()
        {
            var response = await _teacherService.GetRatings(User.Identity.Name);
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View(response.Data);
        }

    }
}
