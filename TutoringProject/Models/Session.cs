using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Column(TypeName = "datetime2")]
        public DateTime Time { get; set; }

        [Required]
        [DisplayName("Online")]
        public bool IsOnline { get; set; }

        [Required]
        [DisplayName("Max Students")]
        public int MaxParticipants { get; set; }



        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [Required]
        public int TutorId { get; set; }

        [ForeignKey("TutorId")]
        public virtual UserAccount.UserAccount Tutor { get; set; }

        public virtual ICollection<UserAccount.UserAccount> Students { get; set; } = new HashSet<UserAccount.UserAccount>();
    }
}