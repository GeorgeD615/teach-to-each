using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.Entity
{
    public class Rating
    {
        public int id { get; set; }
        public int rating_value { get; set; }
        public string review { get; set; }

        public int relation_id { get; set; }
        public TeacherStudent relation { get; set; }


    }
}
