using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.ViewModels.Teacher
{
    public class HomeworkCreateViewModel
    {
        public int relation_id { get; set; }
        public string description { get; set; }
        public DateTime deadline { get; set; }
    }
}
