using System.Linq;
using System.Web.Mvc;
using TutoringProject.Models.Tutor;
using TutoringProject.Models.Session;
using TutoringProject.Models.Student;
using TutoringProject.Models;
using System.Data.Entity;

namespace TutoringProject.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Tutors(string searchTerm)
        {
            using (var db = new TutorContext())
            {
                var tutors = db.Tutors
                 .Include("UserAccount")
                 .Where(t => t.UserAccount.Fname.Contains(searchTerm) ||
                             t.UserAccount.Lname.Contains(searchTerm) ||
                             t.UserAccount.Email.Contains(searchTerm))
                 .ToList();

                return PartialView("~/Views/Shared/_SearchResult.cshtml", tutors);
            }
        }

        public ActionResult Students(string searchTerm)
        {
            using (var db = new TutorContext()) 
            {
                var students = db.Students
                    .Include("UserAccount")
                    .Where(s => s.UserAccount.Fname.Contains(searchTerm) || 
                                s.UserAccount.Lname.Contains(searchTerm) || 
                                s.UserAccount.Email.Contains(searchTerm))
                    .ToList();

                return PartialView("~/Views/Shared/_SearchResult.cshtml", students);
            }
        }

        public ActionResult Sessions(string searchTerm)
        {
            using (var db = new TutorContext()) 
            {
                var sessions = db.Sessions
                    .Include("UserAccount")
                    .Where(s => s.Subject.Contains(searchTerm))
                    .ToList();

                return PartialView("~/Views/Shared/_SearchResult.cshtml", sessions);
            }
        }


    }
}
