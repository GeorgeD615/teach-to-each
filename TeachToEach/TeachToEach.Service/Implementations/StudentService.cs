using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.DAL.Interfaces;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.Enum;
using TeachToEach.Domain.Response;
using TeachToEach.Domain.ViewModels;
using TeachToEach.Domain.ViewModels.Student;
using TeachToEach.Domain.ViewModels.Teacher;
using TeachToEach.Domain.ViewModels.User;
using TeachToEach.Service.Interfaces;

namespace TeachToEach.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Subject> _subjectRepository;
        private readonly IBaseRepository<TeacherSubject> _teacherSubjectRepository;
        private readonly IBaseRepository<TeacherStudent> _teacherStudentRepository;
        private readonly IBaseRepository<StatusOfRelation> _statusOfRelationRepository;
        private readonly IBaseRepository<Homework> _homeworkRepository;
        private readonly IBaseRepository<Rating> _ratingRepository;

        private int _aprove_id;
        private int _request_id;
        private int _reject_id;

        public StudentService(IBaseRepository<User> userRepository,
            IBaseRepository<Subject> subjectRepository,
            IBaseRepository<TeacherSubject> teacherSubjectRepository,
            IBaseRepository<TeacherStudent> teacherStudentRepository,
            IBaseRepository<StatusOfRelation> statusOfRelationRepository,
            IBaseRepository<Homework> homeworkRepository,
            IBaseRepository<Rating> ratingRepository)
        {
            _userRepository = userRepository;
            _subjectRepository = subjectRepository;
            _teacherSubjectRepository = teacherSubjectRepository;
            _teacherStudentRepository = teacherStudentRepository;
            _statusOfRelationRepository = statusOfRelationRepository;
            _homeworkRepository = homeworkRepository;
            _ratingRepository = ratingRepository;

            var status = _statusOfRelationRepository.GetAll();

            _aprove_id = status.FirstOrDefault(st => st.name == "Заявка принята").id;
            _request_id = status.FirstOrDefault(st => st.name == "Заявка на рассмотрении").id;
            _reject_id = status.FirstOrDefault(st => st.name == "Заявка отклонена").id;
        }

        public async Task<IBaseResponse<bool>> CreateRequest(string student_login, string teacher_login, string subject_name)
        {
            try
            {
                var student = _userRepository.GetAll().FirstOrDefault(u => u.login == student_login);
                var teacher = _userRepository.GetAll().FirstOrDefault(u => u.login == teacher_login);
                var subjcet = _subjectRepository.GetAll().FirstOrDefault(s => s.name == subject_name);

                if(student == null || teacher == null || subjcet == null
                    || _teacherStudentRepository.GetAll().Any(r => r.teacher_id == teacher.id && 
                                                                    r.student_id == student.id && 
                                                                    r.subject_id == subjcet.id))
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Description = "Запрос не отправлен",
                        Data = false
                    };
                } 


                await _teacherStudentRepository.Create(new TeacherStudent()
                {
                    student_id = student.id,
                    teacher_id = teacher.id,
                    subject_id = subjcet.id,
                    status_id = _request_id
                });

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = "Запрос отправлен"
                };

            }catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
        public async Task<IBaseResponse<IEnumerable<TeacherProfileViewModel>>> FindTeacher(TeacherFindViewModel teacherFindViewModel)
        {
            try
            {
                var subjects = _subjectRepository.GetAll();
                var a = _teacherSubjectRepository.GetAll();
                var teacher_subject = a.Join(subjects, 
                    a => a.subject_id, 
                    s => s.id, 
                    (t, s) => new { tId = t.teacher_id, Subject = s});

                var teachers = _userRepository.GetAll();

                if (teacherFindViewModel.first_name != null)
                    teachers = teachers.Where(t => t.first_name == teacherFindViewModel.first_name);
                if (teacherFindViewModel.last_name != null)
                    teachers = teachers.Where(t => t.last_name == teacherFindViewModel.last_name);
                if (teacherFindViewModel.login != null)
                    teachers = teachers.Where(t => t.login == teacherFindViewModel.login);
                if (teacherFindViewModel.subject != null)
                    teacher_subject = teacher_subject.Where(r => r.Subject.name == teacherFindViewModel.subject);

                teachers = teachers.Where(t => teacher_subject.Any(r => r.tId == t.id));

                var ratings = _ratingRepository.GetAll();
                var teacherStudents = _teacherStudentRepository.GetAll().Where(r => teachers.Any(t => t.id == r.teacher_id));

                var ratingsTeachers = ratings.Join(teacherStudents,
                    r => r.relation_id,
                    ts => ts.id,
                    (rating, relation) => new { teacher_id = relation.teacher_id, teacher_rating = rating.rating_value });




                var result = teachers.Select(t => new TeacherProfileViewModel()
                {
                    FirstName = t.first_name,
                    LastName = t.last_name,
                    Login = t.login,
                    Age = t.age,
                    Subjects = teacher_subject.Where(r => r.tId == t.id).
                                                Select(r =>new SubjectViewModel() { 
                                                    SubjectName = r.Subject.name
                                                }).ToList(),
                    Id = t.id,
                    AvgRating = (float)ratingsTeachers.Where(r => r.teacher_id == t.id).Select(r => r.teacher_rating).Average()
                }).OrderByDescending(r => r.AvgRating == null ? 0 : r.AvgRating);


                return new BaseResponse<IEnumerable<TeacherProfileViewModel>>()
                {
                    Data = result,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = $"Найдено {result.Count()} учителей"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TeacherProfileViewModel>>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
        public async Task<IBaseResponse<IEnumerable<StudentHomeworkViewModel>>> GetHomewoks(string login)
        {
            try
            {
                var student = _userRepository.GetAll().FirstOrDefault(u => u.login == login);

                if (student == null)
                {
                    return new BaseResponse<IEnumerable<StudentHomeworkViewModel>>()
                    {
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Description = "Профайл не найден"
                    };
                }

                var relations = _teacherStudentRepository.GetAll().Where(r => r.student_id == student.id && r.status_id == _aprove_id).
                    Join(_userRepository.GetAll(),
                    r => r.teacher_id,
                    u => u.id,
                    (relation, teacher) => new { Relation = relation, Teacher = teacher });

                var subjects = _subjectRepository.GetAll();

                var result = _homeworkRepository.GetAll().Join(relations,
                    h => h.relation_id,
                    r => r.Relation.id,
                    (homework, relation) => new StudentHomeworkViewModel()
                    {
                        Teacher = new UserViewModel()
                        {
                            first_name = relation.Teacher.first_name,
                            last_name = relation.Teacher.last_name,
                            age = relation.Teacher.age,
                            email = relation.Teacher.email,
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
                        TeacherComment = homework.teacher_comment,
                        homework_id = homework.id
                    }).ToList();

                return new BaseResponse<IEnumerable<StudentHomeworkViewModel>>()
                {
                    Data = result,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = "Профайл найден"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<StudentHomeworkViewModel>>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
        public async Task<IBaseResponse<StudentProfileViewModel>> GetStudentByLogin(string login)
        {
            try
            {
                var student = _userRepository.GetAll().FirstOrDefault(u => u.login == login);

                if (student == null)
                {
                    return new BaseResponse<StudentProfileViewModel>()
                    {
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Description = "Профайл не найден"
                    };
                }

                var profile = new StudentProfileViewModel()
                {
                    FirstName = student.first_name,
                    LastName = student.last_name,
                    Age = student.age,
                    Login = student.login,
                };

                return new BaseResponse<StudentProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = "Профайл найден"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<StudentProfileViewModel>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Description = "Ошибка"
                };
            }
        }
        public async Task<IBaseResponse<IEnumerable<StudentTeacherInfoViewModel>>> GetTeachers(string login)
        {
            try
            {
                var student = _userRepository.GetAll().FirstOrDefault(u => u.login == login);

                if (student == null)
                {
                    return new BaseResponse<IEnumerable<StudentTeacherInfoViewModel>>()
                    {
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Description = "Профайл не найден"
                    };
                }

                var users = _userRepository.GetAll();
                var teacher_student = _teacherStudentRepository.GetAll().Where(s => s.student_id == student.id);
                var subjects = _subjectRepository.GetAll();
                var ratings = _ratingRepository.GetAll();

                var res = users.Join(teacher_student,
                                    u => u.id,
                                    r => r.teacher_id,
                                    (teacher, relation) => new StudentTeacherInfoViewModel
                                    {
                                        Teacher = new UserViewModel()
                                        {
                                            first_name = teacher.first_name,
                                            last_name = teacher.last_name,
                                            age = teacher.age
                                        },
                                        Subject = new SubjectViewModel()
                                        {
                                            SubjectName = subjects.FirstOrDefault(s => s.id == relation.subject_id).name
                                        },
                                        status_id = relation.status_id,

                                        relation_id = relation.id,

                                        hasRating = ratings.Any(r => r.relation_id == relation.id),

                                        aprove_id = _aprove_id,
                                        reject_id = _reject_id,
                                        request_id = _request_id
                                    }).OrderBy(r => r.status_id).ToList();

                return new BaseResponse<IEnumerable<StudentTeacherInfoViewModel>>()
                {
                    Data = res,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = "Профайл найден"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<StudentTeacherInfoViewModel>>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
        public async Task<IBaseResponse<bool>> UpdateHomework(HomeworkUpdateViewModel homeworkUpdateViewModel)
        {
            try
            {
                var homework = await _homeworkRepository.Get(homeworkUpdateViewModel.id);

                homework.solution = homeworkUpdateViewModel.solution;
                homework.solution_time = DateTime.Now;
                homework.teacher_comment = null;

                bool result = await _homeworkRepository.Edit(homework);

                if (result)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = result,
                        StatusCode = StatusCode.OK,
                        Description = "Домашнее задание обновлено"
                    };
                }
                else
                {
                    return new BaseResponse<bool>()
                    {
                        Data = result,
                        StatusCode = StatusCode.HomeworkNotCreated,
                        Description = "Домашнее задание не обновлено"
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
        public async Task<IBaseResponse<bool>> CreateRating(CreateRatingViewModel createRatingViewModel)
        {
            try
            {
                var rating = new Rating()
                {
                    relation_id = createRatingViewModel.relation_id,
                    rating_value = createRatingViewModel.rating_value,
                    review = createRatingViewModel.review
                };

                var old_rating = _ratingRepository.GetAll().FirstOrDefault(r => r.relation_id == rating.relation_id);
                bool result;
                if (old_rating != null)
                {
                    old_rating.rating_value = rating.rating_value;
                    old_rating.review = rating.review;
                    result = await _ratingRepository.Edit(old_rating);
                }
                else
                {
                    result = await _ratingRepository.Create(rating);
                }

                if (result)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = result,
                        StatusCode = StatusCode.OK,
                        Description = "Отзыв отправлен"
                    };
                }
               
                return new BaseResponse<bool>()
                {
                    Data = result,
                    StatusCode = StatusCode.RatingNotCreated,
                    Description = "Отзыв не отправлен"
                };
                 

            }catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
    }
}
