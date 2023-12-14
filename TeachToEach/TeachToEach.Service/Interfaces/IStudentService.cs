using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Response;
using TeachToEach.Domain.ViewModels;
using TeachToEach.Domain.ViewModels.Student;
using TeachToEach.Domain.ViewModels.Teacher;

namespace TeachToEach.Service.Interfaces
{
    public interface IStudentService
    {
        Task<IBaseResponse<StudentProfileViewModel>> GetStudentByLogin(string login);
        Task<IBaseResponse<IEnumerable<StudentTeacherInfoViewModel>>> GetTeachers(string login);
        Task<IBaseResponse<IEnumerable<StudentHomeworkViewModel>>> GetHomewoks(string login);
        Task<IBaseResponse<bool>> CreateRequest(string student_login, string teacher_login, string subject_name);
        Task<IBaseResponse<IEnumerable<TeacherProfileViewModel>>> FindTeacher(TeacherFindViewModel teacherFindViewModel);
        Task<IBaseResponse<bool>> UpdateHomework(HomeworkUpdateViewModel homeworkUpdateViewModel);

    }
}
