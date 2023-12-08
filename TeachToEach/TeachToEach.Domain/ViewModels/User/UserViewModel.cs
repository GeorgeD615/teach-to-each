using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(2, ErrorMessage = "Имя должно иметь длину больше 2 символов")]
        public string first_name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Укажите фамилию")]
        [MaxLength(20, ErrorMessage = "Фамилия должна иметь длину меньше 20 символов")]
        [MinLength(2, ErrorMessage = "Фамилия должна иметь длину больше 2 символов")]
        public string last_name { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Укажите возраст")]
        [Range(7, 120, ErrorMessage = "Указанный возраст выходит за рамки допустимого диапозона")]
        public int age { get; set; }

        [Display(Name = "email")]
        [Required(ErrorMessage = "Укажите почту")]
        [EmailAddress(ErrorMessage = "Не соответсвует формату почтового адреса")]
        public string email { get; set; }
    }
}
