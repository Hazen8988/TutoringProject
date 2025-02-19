using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutoringProject.Models.Student
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime Birthday { get; set; }



    }
}