using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace TutoringProject.Models.SessionMaterial
{
    [Table("SessionMaterial")]
    public class SessionMaterial
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName {  get; set; }

        public DateTime UploadedAt { get; set; }

        public int TutorId  { get; set; }
        public int SessionId { get; set; }

        //[ForeignKey("TutorId")]
        //public virtual Tutor.Tutor Tutor { get; set; }

        //[ForeignKey("SessionId")]
        //public virtual Session.Session Session { get; set; }
      

    }
}