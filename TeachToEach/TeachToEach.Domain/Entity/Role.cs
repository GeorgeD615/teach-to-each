﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.Entity
{

    public class Role
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<User> users { get; set; }

    }
}
