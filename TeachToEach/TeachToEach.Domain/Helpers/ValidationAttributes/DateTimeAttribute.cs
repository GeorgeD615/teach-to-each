using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.Helpers.ValidationAttributes
{
    public class DateTimeAttribute : ValidationAttribute
    {
        
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            if(value is DateTime)
            {
                return (DateTime)value > DateTime.Now;
            }
            return false;
        }
    }
}
