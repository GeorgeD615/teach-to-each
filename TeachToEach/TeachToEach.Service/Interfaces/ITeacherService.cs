using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Response;
using TeachToEach.Domain.ViewModels;
using TeachToEach.Domain.ViewModels.Teacher;
using TeachToEach.Domain.ViewModels.User;

namespace TeachToEach.Service.Interfaces
{
    public interface ITeacherService
    {
        Task<IBaseResponse<TeacherProfileViewModel>> GetTeacherByLogin(string login);
        Task<IBaseResponse<IEnumerable<TeacherStudentsInfoViewModel>>> GetStudents(string login);
        Task<IBaseResponse<IEnumerable<TeacherHomeworkViewModel>>> GetHomewoks (string login);
        Task<IBaseResponse<bool>> ResponseRequest(int relation_id, int status_id);
        Task<IBaseResponse<bool>> CreateHomework(HomeworkCreateViewModel homeworkCreateViewModel);
        Task<IBaseResponse<bool>> UpdateHomework(HomeworkUpdateViewModel homeworkUpdateViewModel);
        Task<IBaseResponse<IEnumerable<RatingViewModel>>> GetRatings(string login);
        Task<IBaseResponse<bool>> AddSubject(string login, string subject_name);
    }
}
