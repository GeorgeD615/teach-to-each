using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.Entity
{

    public class Subject
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<TeacherStudent> teacher_student_relations { get; set; }
        public List<TeacherSubject> teacher_subject_relation { get; set; } 
    }
}
