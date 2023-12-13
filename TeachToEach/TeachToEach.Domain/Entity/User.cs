using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Enum;

namespace TeachToEach.Domain.Entity
{
    public class User
    {
        public int id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public int age { get; set; }

        public string email { get; set; }

        public int role_id { get; set; }

        public Role role { get; set; }

        public string password { get; set; }

        public string login { get; set; }

        public List<TeacherStudent> relation_as_student { get; set; }
        public List<TeacherStudent> relation_as_teacher { get; set; }
        public List<TeacherSubject> subjects_for_teacher { get; set; }


    }
}
