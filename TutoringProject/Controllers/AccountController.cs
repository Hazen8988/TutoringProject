using System;
using System.Linq;
using System.Web.Mvc;
using TutoringProject.Models;
using TutoringProject.Models.Student;
using TutoringProject.Models.Tutor;
using TutoringProject.Models.UserAccount;
using System.Diagnostics;

namespace TutoringProject.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            var record = new UserRegistrationRecord
            {
                UserAccount = new UserAccount(),
                Tutor = new Tutor(),
                Student = new Student()
            };
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegistrationRecord record)
        {
            record.UserAccount.Role = record.Role;
            ModelState.Remove("UserAccount.Role");

            if (record.Role == "Tutor")
                foreach (var key in ModelState.Keys.Where(k => k.StartsWith("Student.")).ToList())
                {
                    ModelState.Remove(key);
                    Debug.WriteLine(key + " removed key from Student");
                }
            else if (record.Role == "Student")
                foreach (var key in ModelState.Keys.Where(k => k.StartsWith("Tutor.")).ToList())
                {
                    ModelState.Remove(key);
                    Debug.WriteLine(key + " removed key from Tutor");
                }

            if (ModelState.IsValid)
            {
                using (var context = new TutorContext())
                {
                    // Check if already existing user
                    var existingUser = context.UserAccounts.FirstOrDefault(u => u.Email == record.UserAccount.Email);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("Email", "Email already exists");
                        return View(record);
                    }

                    // Add the new user account
                    context.UserAccounts.Add(record.UserAccount);
                    context.SaveChanges();

                    // Assign and save the role-specific data
                    if (record.Role == "Tutor")
                    {
                        record.Tutor.Id = record.UserAccount.Id;
                        context.Tutors.Add(record.Tutor);
                    }
                    else if (record.Role == "Student")
                    {
                        record.Student.Id = record.UserAccount.Id;
                        context.Students.Add(record.Student);
                    }
                    else
                    {
                        ModelState.AddModelError("Role", "Invalid role selected");
                        return View(record);
                    }

                    context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }

            foreach (var key in ModelState.Keys)
            {
                var errors = ModelState[key].Errors;
                foreach (var error in errors)
                {
                    Debug.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                }
            }

            return View(record);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccount userAccount)
        {
            using (var context = new TutorContext())
            {
                if (userAccount.Email == null || userAccount.Password == null)
                {
                    ModelState.AddModelError("", "Email and password are required.");
                    return View(userAccount);
                }

                var user = context.UserAccounts
                    .FirstOrDefault(u => u.Email == userAccount.Email && u.Password == userAccount.Password);
                if (user != null)
                {
                    Session["UserId"] = user.Id;
                    Session["Role"] = user.Role;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(userAccount);
                }
            }
        }
    }
}