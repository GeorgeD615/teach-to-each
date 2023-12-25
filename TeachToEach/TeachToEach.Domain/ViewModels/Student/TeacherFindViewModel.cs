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
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(2, ErrorMessage = "Имя должно иметь длину больше 2 символов")]
        public string? first_name { get; set; }

        [MaxLength(20, ErrorMessage = "Фамилия должна иметь длину меньше 20 символов")]
        [MinLength(2, ErrorMessage = "Фамилия должна иметь длину больше 2 символов")]
        public string? last_name { get; set; }


        [MaxLength(20, ErrorMessage = "Логин должен иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Логин должнен иметь длину больше 3 символов")]
        public string? login { get; set; }

        [Required(ErrorMessage = "Введите предмет")]
        public string? subject { get; set; }        
    }
}
