using System.Linq;
using System.Web.Mvc;
using TutoringProject.Models.Session;
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
                var tutors = db.UserAccounts
                     .Where(u => u.Role == "Tutor")
                     .Where(t => t.Fname.Contains(searchTerm) ||
                                 t.Lname.Contains(searchTerm) ||
                                 t.Email.Contains(searchTerm))
                     .ToList();

                return PartialView("~/Views/Shared/_SearchResult.cshtml", tutors);
            }
        }

        public ActionResult Students(string searchTerm)
        {
            using (var db = new TutorContext()) 
            {
                var students = db.UserAccounts
                    .Where(u => u.Role == "Student")
                    .Where(s => s.Fname.Contains(searchTerm) || 
                                s.Lname.Contains(searchTerm) || 
                                s.Email.Contains(searchTerm))
                    .ToList();

                return PartialView("~/Views/Shared/_SearchResult.cshtml", students);
            }
        }

        public ActionResult Sessions(string searchTerm)
        {
            using (var db = new TutorContext()) 
            {
                var sessions = db.Sessions
                    .Include("Tutor.UserAccount")
                    .Include("Course")
                    .Where(s => s.Course.Name.Contains(searchTerm))
                    .ToList();

                return PartialView("~/Views/Shared/_SearchResult.cshtml", sessions);
            }
        }
    }
}
