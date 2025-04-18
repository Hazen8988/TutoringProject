using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TutoringProject.Models.Tutor;

namespace TutoringProject.Models
{
    public class TutorContext : DbContext
    {
        public TutorContext() : base("TutorDBConnection")
        {
        }

        public DbSet<UserAccount.UserAccount> UserAccounts { get; set; }

        public DbSet<Tutor.Tutor> Tutors { get; set; }

        public DbSet<Student.Student> Students { get; set; }

        public DbSet<Session.Session> Sessions { get; set; }
    }
}