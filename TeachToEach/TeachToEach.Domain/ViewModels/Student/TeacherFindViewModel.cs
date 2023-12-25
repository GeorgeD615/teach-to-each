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
        
        public string? first_name { get; set; }
        public string? last_name { get; set; }


        [MaxLength(20, ErrorMessage = "Логин должен иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Логин должнен иметь длину больше 3 символов")]
        public string? login { get; set; }

        [Required(ErrorMessage = "Введите предмет")]
        public string? subject { get; set; }        
    }
}
