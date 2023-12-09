using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Enum;

namespace TeachToEach.Domain.Entity
{
    [Table(name:"Users")]
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(32)]
        [MinLength(2)]
        [Column("FirstName",TypeName = "character varying(32)")]
        public string first_name { get; set; }

        [Required]
        [MaxLength(32)]
        [MinLength(2)]
        public string last_name { get; set; }

        [Required]
        [Range(8,120)]
        public int age { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }


        public Role role { get; set; }

        [Required]
        [PasswordPropertyText]
        public string password { get; set; }

        [Required]
        [MaxLength(32)]
        [MinLength(5)]
        public string login { get; set; }

    }
}
