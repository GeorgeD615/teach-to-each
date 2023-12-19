using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.ViewModels.Student
{
    public class CreateRatingViewModel
    {
        public int rating_value { get; set; }
        public string review { get; set; }
        public int relation_id { get; set; }
    }
}
