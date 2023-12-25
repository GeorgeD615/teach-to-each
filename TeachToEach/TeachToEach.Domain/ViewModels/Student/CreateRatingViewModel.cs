using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.ViewModels.Student
{
    public class CreateRatingViewModel
    {
        [Required(ErrorMessage = "Поле рейтинг не может быть пустым")]
        [Range(1,5, ErrorMessage = "Оценка должна входить в диапазон от 1 до 5")]
        public int rating_value { get; set; }

        [Required(ErrorMessage = "Поле отзыв не может быть пустым")]
        public string? review { get; set; }
        public int relation_id { get; set; }
    }
}
