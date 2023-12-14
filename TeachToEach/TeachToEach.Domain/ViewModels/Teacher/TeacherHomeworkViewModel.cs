using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.ViewModels.User;

namespace TeachToEach.Domain.ViewModels.Teacher
{
    public class TeacherHomeworkViewModel
    {
        public UserViewModel Student { get; set; }
        public SubjectViewModel Subject { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime SolutionTime { get; set; }
        public bool IsCompleted { get; set; }
        public string Solution { get; set; }
        public string TeacherComment { get; set; }

        public int homework_id { get; set; }
    }
}
