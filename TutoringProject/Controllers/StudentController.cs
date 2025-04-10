using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;
using TutoringProject.Models.Student;


namespace TutoringProject.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new TutorContext())
            {
                var students = db.Students.ToList();
                return View(students);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid) //checks model for required ect..
            {
                using (var db = new TutorContext())
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }

        public ActionResult Edit(int id)
        {
            using (var db = new TutorContext()) 
            {
                var student = db.Students.Find(id);
                if (student == null) 
                { 
                    return HttpNotFound();
                }
                return View(student);
            }
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            using (var db = new TutorContext()) 
            {
                if (ModelState.IsValid) 
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(student);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db = new TutorContext())
            {
                var student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }

                db.Students.Remove(student);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new TutorContext())
            {
                var student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
        }
    }
}