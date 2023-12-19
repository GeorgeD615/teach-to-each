using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.ViewModels.User;

namespace TeachToEach.Domain.ViewModels.Teacher
{
    public class RatingViewModel
    {
        public int rating_value { get; set; }
        public string review { get; set; }
        public UserViewModel student { get; set; }

    }
}
