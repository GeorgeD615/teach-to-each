using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.Entity
{
    public class StatusOfRelation
    {
        public int id { get; set; }// 1 - рассмотрение запроса, 2 - запрос одобрен, 3 - запрос отклонён 
        public string name { get; set; }
        public List<TeacherStudent> relations { get; set; }
    }
}
