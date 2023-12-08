using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(2, ErrorMessage = "Имя должно иметь длину больше 2 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [MaxLength(20, ErrorMessage = "Фамилия должна иметь длину меньше 20 символов")]
        [MinLength(2, ErrorMessage = "Фамилия должна иметь длину больше 2 символов")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Укажите возраст")]
        [Range(7,120, ErrorMessage = "Указанный возраст выходит за рамки допустимого диапозона")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Укажите почту")]
        [EmailAddress(ErrorMessage = "Не соответсвует формату почтового адреса")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(20, ErrorMessage = "Логин должен иметь длину меньше 20 символов")]
        [MinLength(2, ErrorMessage = "Логин должен иметь длину больше 2 символов")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
