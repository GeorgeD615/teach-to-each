using System.ComponentModel.DataAnnotations;

namespace TeachToEach.Domain.Enum
{
    public enum Subject
    {
        [Display(Name = "Математика")]
        Mathematics = 0,
        [Display(Name = "Литература")]
        Literature = 1,
        [Display(Name = "Информатика")]
        Informatics = 2,
        [Display(Name = "Биология")]
        Biology = 3,
        [Display(Name = "История")]
        History = 4
    }
}
