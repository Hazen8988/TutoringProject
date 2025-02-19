using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TutoringProject.Models.Student
{
    public class StudentContext:DbContext
    {
        public StudentContext() : base("TutorDBConnection") { }

        public DbSet<Student> Students { get; set; }
    }
}