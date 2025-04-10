using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TutoringProject.Models.Tutor
{
    public class TutorContext:DbContext
    {
        // remember to add TutorDBConnection into web config
        public TutorContext() : base("TutorDBConnection") { }

        public DbSet<Tutor> Tutors { get; set; }
    }
}