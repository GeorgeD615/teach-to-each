using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.Entity
{
    public class TeacherSubject
    {
        public int id { get; set; }

        public int teacher_id { get; set; }
        public User teacher {  get; set; }
        public int subject_id { get; set; }
        public Subject subject { get; set; }

    }
}
