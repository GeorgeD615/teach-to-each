using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.Entity
{
    public class TeacherStudent
    {
        public int id { get; set; }
        public int teacher_id { get; set; }
        public User teacher { get; set; }
        public int student_id { get; set; }
        public User student { get; set; }
        public int subject_id { get; set; }
        public Subject subject { get; set; }
        public int status_id { get; set; }
        public StatusOfRelation status { get; set; }
        public Rating rating { get; set; }
        public List<Homework> homeworks { get; set; }
    }
}
