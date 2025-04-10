using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;
using TutoringProject.Models.Tutor;

namespace TutoringProject.Controllers
{
    public class TutorController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new TutorContext())
            {
                var tutors = db.Tutors.ToList();
                return View(tutors);
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new TutorContext())
            {
                var tutor = db.Tutors.Find(id);
                if (tutor == null)
                {
                    return HttpNotFound();
                }

                return View(tutor);
            }
        }

        [HttpPost]
        public ActionResult Edit(Tutor tutor)
        {
            using (var db = new TutorContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tutor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tutor);
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new TutorContext())
            {
                var tutor = db.Tutors.Find(id);
                if (tutor == null)
                {
                    return HttpNotFound();
                }
                return View(tutor);
            }
        }
    }
}