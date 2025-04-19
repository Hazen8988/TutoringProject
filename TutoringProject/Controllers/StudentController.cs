using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;


namespace TutoringProject.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new TutorContext())
            {
                var students = db.UserAccounts
                    .Where(u => u.Role == "Student")
                    .ToList();
                return View(students);
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new TutorContext())
            {
                var student = db.UserAccounts
                    .Where(u => u.Role == "Student")
                    .ToList()
                    .Find(s => s.Id == id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
        }
    }
}