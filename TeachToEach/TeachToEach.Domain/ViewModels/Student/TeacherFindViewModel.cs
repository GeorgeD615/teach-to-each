using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.ViewModels.Student
{
    public class TeacherFindViewModel
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string login { get; set; }
        public string subject { get; set; }        
    }
}
