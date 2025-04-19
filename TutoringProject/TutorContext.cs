using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TutoringProject.Models.Session;
using TutoringProject.Models.UserAccount;

namespace TutoringProject.Models
{
    public class TutorContext : DbContext
    {
        public TutorContext() : base("TutorDBConnection")
        {
        }

        public DbSet<UserAccount.UserAccount> UserAccounts { get; set; }

        public DbSet<Session.Session> Sessions { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Session.Session>()
                .HasMany(s => s.Students)
                .WithMany(s => s.Sessions)
                .Map(cs =>
                {
                    cs.MapLeftKey("StudentId");
                    cs.MapRightKey("SessionId");
                    cs.ToTable("StudentSession");
                });

            modelBuilder.Entity<Session.Session>()
                .HasRequired(s => s.Tutor)
                .WithMany()
                .HasForeignKey(s => s.TutorId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Session.Session>()
                .HasRequired(s => s.Course)
                .WithMany()
                .HasForeignKey(s => s.CourseId)
                .WillCascadeOnDelete(true);
        }
    }
}