using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutoringProject.Models.Session
{
    [Table("Session")]
    public class Session
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
        [Required]
        public string Subject { get; set; }

        [Required]
        public int StudentId { get; set; }
        [Required]
        public int TutorId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student.Student Student { get; set; }

        [ForeignKey("TutorId")]
        public virtual Tutor.Tutor Tutor { get; set; }

    }
}