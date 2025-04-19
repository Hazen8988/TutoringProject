using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;
using TutoringProject.Models.UserAccount;


namespace TutoringProject.Controllers
{
    public class TutorController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new TutorContext())
            {
                var tutors = db.UserAccounts
                    .Where(u => u.Role == "Tutor")
                    .ToList();
                return View(tutors);
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new TutorContext())
            {
                var tutor = db.UserAccounts
                    .Where(u => u.Role == "Tutor")
                    .ToList()
                    .Find(t => t.Id == id);

                if (tutor == null)
                {
                    return HttpNotFound();
                }

                var sessions = db.Sessions
                    .Include(s => s.Course)
                    .Where(s => s.TutorId == id)
                    .ToList();

                ViewBag.Sessions = sessions;
                return View(tutor);
            }
        }

////search bar action method 
//        public ActionResult Search(string query)
//        {
//            using (var db = new TutorContext())
//            {
//                {
//                    var tutors = db.Tutors
//                        .Where(t => t.Fname.Contains(query) || t.Lname.Contains(query))
//                        .ToList();
//                    return PartialView("_TutorList", tutors);

//                }
//            }
//        }
    }
}