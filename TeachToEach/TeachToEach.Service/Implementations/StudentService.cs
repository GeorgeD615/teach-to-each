﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.DAL.Interfaces;
using TeachToEach.Domain.Entity;
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

        private int _aprove_id;
        private int _request_id;
        private int _reject_id;

        public StudentService(IBaseRepository<User> userRepository,
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
                        TeacherComment = homework.teacher_comment
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

                                        aprove_id = _aprove_id,
                                        reject_id = _reject_id,
                                        request_id = _request_id
                                    }).OrderByDescending(r => r.status_id).ToList();

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
    }
}