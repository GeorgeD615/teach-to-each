using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.Helpers.ValidationAttributes;

namespace TeachToEach.Domain.ViewModels
{
    public class HomeworkUpdateViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Поле домашнее задание должно быть заполнено")]
        public string? description { get; set; }

        [DateTime(ErrorMessage = "Введите непрошедшую дату")]
        public DateTime? deadline { get; set; }
        public DateTime? solution_time { get; set; }
        public bool is_completed { get; set; }
        public string? solution { get; set; }
        public string? teacher_comment { get; set; }
    }
}
