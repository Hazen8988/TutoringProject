using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models.Session;
using TutoringProject.Models.Student;
using TutoringProject.Models.Tutor;


namespace TutoringProject.Controllers
{
    public class SessionController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new SessionContext())
            using (var dbStudent = new StudentContext())
            {
                var sessions = db.Sessions.Include(s => s.Student).ToList();
                return View(sessions);
            }
        }
        //GET : Session/Create for student and tutors dropdown list
        public ActionResult Create()
        {
            if (Session["UserId"] == null || !Session["Role"].Equals("Tutor"))
            {
                return RedirectToAction("Index", "Home");
            } 

            using (var db = new TutorContext()) 
            using (var dbStudent = new StudentContext())
            { 
                ViewBag.Tutors = db.Tutors.ToList().Select(t => new
                {
                    Id = t.Id,
                    FnameLname = t.Fname + " " + t.Lname
                }).ToList();
                ViewBag.Students = dbStudent.Students.ToList().Select(s => new
                {
                    Id = s.Id,
                    FnameLname = s.Fname + " " + s.Lname
                }).ToList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Session session)
        {
            if (Session["UserId"] == null || !Session["Role"].Equals("Tutor"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                using (var db = new SessionContext())
                {
                    db.Sessions.Add(session);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(session);
        }

        public ActionResult Edit(int id)
        {
            using (var db = new SessionContext())
            using (var studentdb = new StudentContext())
            using (var tutordb = new TutorContext())
            {
                var session = db.Sessions.Find(id);
                if (session == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Tutors = tutordb.Tutors.ToList().Select(t => new
                {
                    Id = t.Id,
                    FnameLname = t.Fname + " " + t.Lname
                }).ToList();
                ViewBag.Students = studentdb.Students.ToList().Select(s => new
                {
                    Id = s.Id,
                    FnameLname = s.Fname + " " + s.Lname
                }).ToList();
                return View(session);
            }
        }

        [HttpPost]
        public ActionResult Edit(Session session)
        {
            using (var db = new SessionContext())
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
        public ActionResult Delete(int id)
        {
            using (var db = new SessionContext())
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
            using (var db = new SessionContext())
            {
                var session = db.Sessions.Find(id);
                if (session == null)
                {
                    return HttpNotFound();
                }
                return View(session);
            }
        }
    }
}