using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Entity;

namespace TeachToEach.Domain.ViewModels
{
    public class HomeworkUpdateViewModel
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime? deadline { get; set; }
        public DateTime? solution_time { get; set; }
        public bool is_completed { get; set; }
        public string solution { get; set; }
        public string teacher_comment { get; set; }
    }
}
