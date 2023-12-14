using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.Entity
{
    public class Homework
    {
        public int id { get; set; }
        public int relation_id { get; set; }
        public TeacherStudent relation { get; set; }
        public string description { get; set; }
        public DateTime? deadline { get; set; }
        public DateTime? solution_time { get; set; }
        public bool is_completed { get; set; }
        public string? solution { get; set; }
        public string? teacher_comment { get; set; }
    }
}
