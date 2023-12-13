using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.DAL.Interfaces;
using TeachToEach.DAL.Repositories;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.Response;
using TeachToEach.Domain.ViewModels;
using TeachToEach.Domain.ViewModels.Teacher;
using TeachToEach.Domain.ViewModels.User;
using TeachToEach.Service.Interfaces;

namespace TeachToEach.Service.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Subject> _subjectRepository;
        private readonly IBaseRepository<TeacherSubject> _teacherSubjectRepository;
        private readonly IBaseRepository<TeacherStudent> _teacherStudentRepository;
        private readonly IBaseRepository<StatusOfRelation> _statusOfRelationRepository;
        private readonly IBaseRepository<Homework> _homeworkRepository;

        private int _aprove_id;
        private int _request_id;
        private int _reject_id;

        public TeacherService(IBaseRepository<User> userRepository, 
            IBaseRepository<Subject> subjectRepository, 
            IBaseRepository<TeacherSubject> teacherSubjectRepository,
            IBaseRepository<TeacherStudent> teacherStudentRepository,
            IBaseRepository<StatusOfRelation> statusOfRelationRepository,
            IBaseRepository<Homework> homeworkRepository)
        {
            _userRepository = userRepository;
            _subjectRepository = subjectRepository;
            _teacherSubjectRepository = teacherSubjectRepository;
            _teacherStudentRepository = teacherStudentRepository;
            _statusOfRelationRepository = statusOfRelationRepository;
            _homeworkRepository = homeworkRepository;

            var status = _statusOfRelationRepository.GetAll();

            _aprove_id = status.FirstOrDefault(st => st.name == "Заявка принята").id;
            _request_id = status.FirstOrDefault(st => st.name == "Заявка на рассмотрении").id;
            _reject_id = status.FirstOrDefault(st => st.name == "Заявка отклонена").id;
        }

        public async Task<IBaseResponse<IEnumerable<TeacherHomeworkViewModel>>> GetHomewoks(string login)
        {
            try
            {
                var teacher = _userRepository.GetAll().FirstOrDefault(u => u.login == login);

                if (teacher == null)
                {
                    return new BaseResponse<IEnumerable<TeacherHomeworkViewModel>>()
                    {
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Description = "Профайл не найден"
                    };
                }

                var relations = _teacherStudentRepository.GetAll().Where(r => r.teacher_id == teacher.id && r.status_id == _aprove_id).
                    Join(_userRepository.GetAll(),
                    r => r.student_id,
                    u => u.id,
                    (relation, student) => new { Relation = relation, Student = student });

                var subjects = _subjectRepository.GetAll();

                var result = _homeworkRepository.GetAll().Join(relations,
                    h => h.relation_id,
                    r => r.Relation.id,
                    (homework, relation) => new TeacherHomeworkViewModel()
                    {
                        Student = new UserViewModel()
                        {
                            first_name = relation.Student.first_name,
                            last_name = relation.Student.last_name,
                            age = relation.Student.age,
                            email = relation.Student.email,
                        },
                        Subject = new SubjectViewModel()
                        {
                            SubjectName = subjects.FirstOrDefault(s => s.id == relation.Relation.subject_id).name
                        },
                        Description = homework.description,
                        Deadline = homework.deadline,
                        SolutionTime = homework.solution_time,
                        IsCompleted = homework.is_completed,
                        Solution = homework.solution,
                        TeacherComment = homework.teacher_comment
                    }).ToList();

                return new BaseResponse<IEnumerable<TeacherHomeworkViewModel>>()
                {
                    Data = result,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = "Профайл найден"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TeacherHomeworkViewModel>>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<TeacherStudentsInfoViewModel>>> GetStudents(string login)
        {
            try
            {
                var teacher = _userRepository.GetAll().FirstOrDefault(u => u.login == login);

                if (teacher == null)
                {
                    return new BaseResponse<IEnumerable<TeacherStudentsInfoViewModel>> ()
                    {
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Description = "Профайл не найден"
                    };
                }

                var users = _userRepository.GetAll();
                var teacher_student = _teacherStudentRepository.GetAll().Where(s => s.teacher_id == teacher.id);
                var subjects = _subjectRepository.GetAll();

                var res = users.Join(teacher_student,
                                    u => u.id,
                                    r => r.student_id,
                                    (student, relation) => new TeacherStudentsInfoViewModel{
                                        Student = new UserViewModel()
                                        {
                                            first_name = student.first_name,
                                            last_name = student.last_name,
                                            age = student.age
                                        },
                                        Subject = new SubjectViewModel()
                                        {
                                            SubjectName = subjects.FirstOrDefault(s => s.id == relation.subject_id).name
                                        },
                                        status_id = relation.status_id,

                                        aprove_id = _aprove_id,
                                        reject_id = _reject_id,
                                        request_id = _request_id
                                    }).OrderByDescending(r => r.status_id).ToList();
                
                return new BaseResponse<IEnumerable<TeacherStudentsInfoViewModel>>()
                {
                    Data = res,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = "Профайл найден"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TeacherStudentsInfoViewModel>>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }

        public async Task<IBaseResponse<TeacherProfileViewModel>> GetTeacherByLogin(string login)
        {
            try
            {
                var teacher = _userRepository.GetAll().FirstOrDefault(u => u.login == login);

                if(teacher == null)
                {
                    return new BaseResponse<TeacherProfileViewModel>() {
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Description = "Профайл не найден"
                    };
                }

                var subjects = _subjectRepository.GetAll();
                var teacher_subjects = _teacherSubjectRepository.GetAll().Where(r => r.teacher_id == teacher.id)
                    .Select(r => r.subject_id).Select(id => subjects.FirstOrDefault(s => s.id == id)).
                    Select(s => new SubjectViewModel() { SubjectName = s.name}).ToList();

                var profile = new TeacherProfileViewModel()
                {
                    FirstName = teacher.first_name,
                    LastName = teacher.last_name,
                    Age = teacher.age,
                    Login = teacher.login,
                    Subjects = teacher_subjects
                };

                return new BaseResponse<TeacherProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = "Профайл найден"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<TeacherProfileViewModel>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Description = "Ошибка"
                };
            }
        }

        
    }
}
