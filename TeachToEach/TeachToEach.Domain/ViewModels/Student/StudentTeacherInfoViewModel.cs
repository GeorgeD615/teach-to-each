using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.ViewModels.User;

namespace TeachToEach.Domain.ViewModels.Student
{
    public class StudentTeacherInfoViewModel
    {
        public UserViewModel Teacher { get; set; }
        public SubjectViewModel Subject { get; set; }

        public int status_id { get; set; }

        public int aprove_id { get; set; }
        public int request_id { get; set; }
        public int reject_id { get; set; }
    }
}
