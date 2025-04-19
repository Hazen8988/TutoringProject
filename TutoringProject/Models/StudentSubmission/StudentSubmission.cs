using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace TutoringProject.Models.StudentSubmission
{
    [Table("StudentSubmission")]
    public class StudentSubmission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        public DateTime SubmittedAt { get; set; }
        public int StudentId { get; set; }

        //[ForeignKey("StudentId")]
        //public virtual Student.Student Student { get; set; }

        public int SessionId { get; set; }

        //[ForeignKey("SessionId")]
        //public virtual Session.Session Session { get; set; }

    }
}