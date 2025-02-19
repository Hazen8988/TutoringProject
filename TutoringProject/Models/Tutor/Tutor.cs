using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutoringProject.Models.Tutor
{
    [Table("Tutor")]
    public class Tutor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public string Delivery { get; set; }
        [Required]
        public double Salary { get; set; }

        [Required]
        public string Email { get; set; }



    }
}