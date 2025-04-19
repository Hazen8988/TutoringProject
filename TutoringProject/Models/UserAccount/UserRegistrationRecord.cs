using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoringProject.Models.UserAccount
{
    public class UserRegistrationRecord
    {
        public UserAccount UserAccount { get; set; }

        public string Role { get; set; } // "Tutor" or "Student"
    }
}