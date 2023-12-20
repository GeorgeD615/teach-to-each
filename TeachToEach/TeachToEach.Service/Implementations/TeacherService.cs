using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.DAL.Interfaces;
using TeachToEach.DAL.Repositories;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.Enum;
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
        private readonly IBaseRepository<Rating> _ratingRepository;

        private int _aprove_id;
        private int _request_id;
        private int _reject_id;

        public TeacherService(IBaseRepository<User> userRepository, 
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

        public async Task<IBaseResponse<bool>> CreateHomework(HomeworkCreateViewModel homeworkCreateViewModel)
        {
            try
            {
                var homework = new Homework()
                {
                    relation_id = homeworkCreateViewModel.relation_id,
                    description = homeworkCreateViewModel.description,
                    deadline = homeworkCreateViewModel.deadline
                };
                
                bool result = await _homeworkRepository.Create(homework);

                if (result)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = result,
                        StatusCode = StatusCode.OK,
                        Description = "Домашнее задание добавлено"
                    };
                }
                else
                {
                    return new BaseResponse<bool>()
                    {
                        Data = result,
                        StatusCode = StatusCode.HomeworkNotCreated,
                        Description = "Домашнее задание не добавлено"
                    };
                }
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
                        TeacherComment = homework.teacher_comment,
                        homework_id = homework.id
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
                                        relation_id = relation.id,

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

        public async Task<IBaseResponse<bool>> ResponseRequest(int relation_id, int status_id)
        {
            try
            {
                var relation = await _teacherStudentRepository.Get(relation_id);
                if(relation == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Description = "Отношение не найдено"
                    };
                }

                relation.status_id = status_id;

                bool result = await _teacherStudentRepository.Edit(relation);

                return new BaseResponse<bool>()
                {
                    Data = result,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = "Статус изменён"
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

        public async Task<IBaseResponse<bool>> UpdateHomework(HomeworkUpdateViewModel homeworkUpdateViewModel)
        {
            try
            {
                var homework = await _homeworkRepository.Get(homeworkUpdateViewModel.id);

                homework.description = homeworkUpdateViewModel.description;
                homework.deadline = homeworkUpdateViewModel.deadline;
                homework.is_completed = homeworkUpdateViewModel.is_completed;
                homework.teacher_comment = homeworkUpdateViewModel.teacher_comment;

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

        public async Task<IBaseResponse<IEnumerable<RatingViewModel>>> GetRatings(string login)
        {
            try
            {
                var users = _userRepository.GetAll();
                var teacher = users.FirstOrDefault(x => x.login == login);
                if(teacher == null)
                {
                    return new BaseResponse<IEnumerable<RatingViewModel>>()
                    {
                        Data = null,
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользовател не найден"
                    };
                }

                var lessons = _teacherStudentRepository.GetAll().Where(r => r.teacher_id == teacher.id);
                var ratings = _ratingRepository.GetAll();
                var subjects = _subjectRepository.GetAll();

                var ratings_relation = lessons.Join(ratings,
                    l => l.id,
                    r => r.relation_id,
                    (lesson, rating) => new { student_id = lesson.student_id, 
                                            rating_value = rating.rating_value, 
                                            review = rating.review, 
                                            subject_id = lesson.subject_id});

                var result = ratings_relation.Select(item => new RatingViewModel()
                {
                    rating_value = item.rating_value,
                    review = item.review,
                    student = users.FirstOrDefault(s => s.id == item.student_id).first_name,
                    subject = new SubjectViewModel() { SubjectName = subjects.FirstOrDefault(s => s.id == item.subject_id).name }
                });

                return new BaseResponse<IEnumerable<RatingViewModel>>(){
                    Data = result,
                    StatusCode = StatusCode.OK,
                    Description = $"Найдено {result.Count()} отзывов"
                };

            }catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<RatingViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
    }
}
