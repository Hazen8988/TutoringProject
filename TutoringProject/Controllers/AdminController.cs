using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;

namespace TutoringProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Users()
        {
            using (var context = new TutorContext())
            {
                var users = context.UserAccounts.ToList();
                return View(users);
            }
        }

        [HttpPost] // to promote a user to admin
        public ActionResult MakeAdmin(int id)
        {
            using (var context = new TutorContext())
            {
                var user = context.UserAccounts.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    ViewBag.Message = "User promoted to admin successfully";
                    user.Role = "Admin";
                    context.SaveChanges();
                }
                else
                {
                    ViewBag.Message = "User not found";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            using (var context = new TutorContext())
            {
                var user = context.UserAccounts.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    context.Sessions.RemoveRange(context.Sessions.Where(s => s.TutorId == id));

                    ViewBag.Message = "User deleted successfully";
                    context.UserAccounts.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    ViewBag.Message = "User not found";
                }
                return RedirectToAction("Users");
            }
        }


        [HttpGet]
        public ActionResult Courses()
        {
            using (var context = new TutorContext())
            {
                var courses = context.Courses.ToList();
                return View(courses);
            }
        }

        [HttpPost]
        public ActionResult Course(Course course)
        {
            using (var context = new TutorContext())
            {
                if (ModelState.IsValid)
                {
                    context.Courses.Add(course);
                    context.SaveChanges();
                    ViewBag.Message = "Course added successfully";
                }
                else
                {
                    ViewBag.Message = "Error adding course";
                }
            }
            return RedirectToAction("Courses");
        }

        [HttpPost]
        public ActionResult DeleteCourse(int id)
        {
            using (var context = new TutorContext())
            {
                var course = context.Courses.FirstOrDefault(c => c.Id == id);
                if (course != null)
                {
                    context.Courses.Remove(course);
                    context.SaveChanges();
                    ViewBag.Message = "Course deleted successfully";
                }
                else
                {
                    ViewBag.Message = "Course not found";
                }
            }
            return RedirectToAction("Courses");
        }

    }
}