using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;

namespace TutoringProject.Controllers
{
    public class CourseController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (var context = new TutorContext())
            {
                var courses = context.Courses.ToList();
                return View(courses);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var context = new TutorContext())
            {
                var course = context.Courses.FirstOrDefault(c => c.Id == id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return View(course);
            }
        }
    }
}