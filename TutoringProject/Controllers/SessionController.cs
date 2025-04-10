using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;
using TutoringProject.Models.Session;
using TutoringProject.Models.Student;
using TutoringProject.Models.Tutor;


namespace TutoringProject.Controllers
{
    public class SessionController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new TutorContext())
            {
                var sessions = db.Sessions.Include(s => s.Student).ToList();
                return View(sessions);
            }
        }
        //GET : Session/Create for student and tutors dropdown list
        public ActionResult Create()
        {
            using (var db = new TutorContext()) 
            { 
                ViewBag.Tutors = db.Tutors.ToList().Select(t => new
                {
                    Id = t.Id,
                    FnameLname = t.UserAccount.Fname + " " + t.UserAccount.Lname
                }).ToList();
                
                ViewBag.Students = db.Students.ToList().Select(s => new
                {
                    Id = s.Id,
                    FnameLname = s.UserAccount.Fname + " " + s.UserAccount.Lname
                }).ToList();
            }
            return View();
        }

        [HttpPost]
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

        public ActionResult Edit(int id)
        {
            using (var db = new TutorContext())
            {
                var session = db.Sessions.Find(id);
                if (session == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Tutors = db.Tutors.ToList().Select(t => new
                {
                    Id = t.Id,
                    FnameLname = t.UserAccount.Fname + " " + t.UserAccount.Lname
                }).ToList();
                ViewBag.Students = db.Students.ToList().Select(s => new
                {
                    Id = s.Id,
                    FnameLname = s.UserAccount.Fname + " " + s.UserAccount.Lname
                }).ToList();
                return View(session);
            }
        }

        [HttpPost]
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
        public ActionResult Delete(int id)
        {
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