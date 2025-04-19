using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;
using TutoringProject.Models.Session;
using TutoringProject.Models.UserAccount;


namespace TutoringProject.Controllers
{
    public class SessionController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new TutorContext())
            {
                var sessions = db.Sessions
                    .Include(s => s.Course)
                    .Include(s => s.Tutor)
                    .ToList();
                return View(sessions);
            }
        }
        //GET : Session/Create for student and tutors dropdown list
        [Authorize(Roles = "Tutor,Admin")]
        public ActionResult Create(int id)
        {
            int userId = (int)Session["UserId"];

            using (var db = new TutorContext()) 
            {
                Course course = db.Courses.Find(id);
                if (course == null)
                {
                    return HttpNotFound();
                }

                UserAccount tutor = db.UserAccounts
                    .FirstOrDefault(t => t.Id == userId);

                ViewBag.Course = course;
                ViewBag.Tutor = tutor;
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Tutor,Admin")]
        public ActionResult Create(Session session)
        {
            if (ModelState.IsValid)
            {
                using (var db = new TutorContext())
                {
                    db.Sessions.Add(session);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(session);
        }

        [HttpGet]
        [Authorize(Roles = "Tutor,Admin")]
        public ActionResult Edit(int id)
        {
            using (var db = new TutorContext())
            {
                var session = db.Sessions.Find(id);
                if (session == null)
                {
                    return HttpNotFound();
                }
                List<UserAccount> userAccounts = db.UserAccounts.ToList();

                ViewBag.Tutors = userAccounts.Where(u => u.Role == "Tutor").ToList()
                    .Select(t => new
                    {
                        Id = t.Id,
                        FnameLname = t.Fname + " " + t.Lname
                    }).ToList();

                ViewBag.Students = userAccounts.Where(u => u.Role == "Student").ToList()
                    .Select(s => new
                    {
                        Id = s.Id,
                        FnameLname = s.Fname + " " + s.Lname
                    }).ToList();
                
                return View(session);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Tutor,Admin")]
        public ActionResult Edit(Session session)
        {
            using (var db = new TutorContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(session).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(session);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Tutor,Admin")]
        public ActionResult Delete(int id)
        {
            if (Session["UserId"] == null || !Session["Role"].Equals("Tutor"))
            {
                return RedirectToAction("Index", "Home");
            }

            using (var db = new TutorContext())
            {
                var session = db.Sessions.Find(id);
                if (session == null)
                {
                    return HttpNotFound();
                }

                db.Sessions.Remove(session);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new TutorContext())
            {
                var session = db.Sessions
                    .Include(s => s.Course)
                    .Include(s => s.Tutor)
                    .Include(s => s.Students)
                    .ToList().Find(s => id == s.Id);
                if (session == null)
                {
                    return HttpNotFound();
                }
                return View(session);
            }
        }
    }
}