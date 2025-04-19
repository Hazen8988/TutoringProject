using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutoringProject.Models.Tutor
{
    using TutoringProject.Models.UserAccount;

    [Table("Tutor")]
    public class Tutor
    {
        [Key]
        [ForeignKey("UserAccount")]
        public int Id { get; set; }
        public virtual UserAccount UserAccount { get; set; }

        [Required(ErrorMessage = "Delivery method is required")]
        [StringLength(100, ErrorMessage = "Delivery method cannot exceed 100 characters")]
        public string Delivery { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
        public double Salary { get; set; }
    }
}