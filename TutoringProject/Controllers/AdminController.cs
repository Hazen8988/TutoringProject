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
        public ActionResult PromoteUser(string email)
        {
            using (var context = new TutorContext())
            {
                var user = context.UserAccounts.FirstOrDefault(u => u.Email == email);
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

        [HttpDelete]
        public ActionResult DeleteUserByEmail(string email)
        {
            using (var context = new TutorContext())
            {
                var user = context.UserAccounts.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
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
}