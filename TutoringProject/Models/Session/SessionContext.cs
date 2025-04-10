using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TutoringProject.Models.Session
{
    public class SessionContext:DbContext
    {
        public SessionContext() :base("TutorDBConnection") { }
        public DbSet<Session> Sessions { get; set; }
    }
}