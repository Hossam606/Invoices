using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int? Id { get; set; }

        [StringLength(50)]

        //[Required]
        public string FullName { get; set; }

        [StringLength(50)]
        public string  UserName { get; set; }

        [StringLength(50)]
        //[EmailAddress]
        //[Required]
        public string  Email { get; set; }

        //[StringLength(50)]
        //[Required]
        //[PasswordPropertyText]
        public string  Password { get; set; }

        [StringLength(50)]
        //[Phone]
        //[Required]
        public string  Phone { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
