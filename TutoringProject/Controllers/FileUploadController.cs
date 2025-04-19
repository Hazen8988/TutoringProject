using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;
using TutoringProject.Models.SessionMaterial;
using TutoringProject.Models.Student;
using TutoringProject.Models.StudentSubmission;

namespace TutoringProject.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload

        [HttpPost]
        public ActionResult UploadMaterial(HttpPostedFile file, int sessionId)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("/Uploads/SessionMaterials/"), fileName);
                file.SaveAs(path);

                using (var db = new TutorContext())
                {
                    var material = new SessionMaterial
                    {
                        FileName = fileName,
                        SessionId = sessionId,
                        TutorId = (int)Session["UserId"], //validation for tutor login
                        UploadedAt = DateTime.Now
                    };
                    db.SessionMaterials.Add(material);
                    db.SaveChanges();
                }

                TempData["UploadSuccess"] = "Material uploaded successfully!";
                return RedirectToAction("Details", "Session", new { id = sessionId });
            }
            TempData["UploadError"] = "Upload Error";
            return RedirectToAction("Details", "Session", new { id = sessionId });
        }

        [HttpPost]
        public ActionResult UploadSubmission(HttpPostedFileBase file)

        {
            int studentId = (int)Session["UserId"]; //use session for student login to save and use for redirction
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/UploadSubmission/StudentSubmissions/"), fileName);
                file.SaveAs(path);

                using (var db = new TutorContext())
                {
                    var submission = new StudentSubmission
                    {
                        FileName = fileName,
                        StudentId = studentId,
                        SubmittedAt = DateTime.Now
                    };
                    db.StudentSubmissions.Add(submission);
                    db.SaveChanges();
                }
                TempData["UploadSuccess"] = "Your assignment was submitted.";
                return RedirectToAction("Details", "Student", new { id = studentId });

            }
            TempData["UploadError"] = "Please select a file.";
            return RedirectToAction("Details", "Student", new { id = studentId });

        }

    }
}