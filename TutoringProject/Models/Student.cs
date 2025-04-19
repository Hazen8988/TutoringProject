using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutoringProject.Models.Student
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [ForeignKey("UserAccount")]
        public int Id { get; set; }
        public virtual UserAccount.UserAccount UserAccount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime Birthday { get; set; }
    }
}