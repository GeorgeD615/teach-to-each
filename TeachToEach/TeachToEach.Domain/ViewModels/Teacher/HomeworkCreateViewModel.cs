using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.ViewModels.Teacher
{
    public class HomeworkCreateViewModel
    {
        public int relation_id { get; set; }

        [Required(ErrorMessage = "Поле домашнее задание должно быть заполнено")]
        public string? description { get; set; }
        public DateTime deadline { get; set; }
    }
}
