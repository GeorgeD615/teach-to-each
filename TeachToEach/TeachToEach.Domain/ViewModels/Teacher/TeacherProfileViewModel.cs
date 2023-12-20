using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.ViewModels.Account;

namespace TeachToEach.Domain.ViewModels.Teacher
{
    public class TeacherProfileViewModel
    {
        public string FirstName;
        public string LastName;
        public int Age;
        public string Login;
        public List<SubjectViewModel> Subjects;
        public int Id;
        public float? AvgRating;
    }
}
