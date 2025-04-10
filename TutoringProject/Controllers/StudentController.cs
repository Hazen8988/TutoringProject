using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models.Student;


namespace TutoringProject.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new StudentContext())
            {
                var students = db.Students.ToList();
                return View(students);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            using (var db = new StudentContext()) 
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
            using (var db = new StudentContext()) 
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

        public ActionResult Details(int id)
        {
            using (var db = new StudentContext())
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