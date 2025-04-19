using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;
using TutoringProject.Models.SessionMaterial;
using TutoringProject.Models.StudentSubmission;

namespace TutoringProject.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload

        [Authorize(Roles = "Tutor")]
        [HttpPost]
        public ActionResult UploadMaterial(HttpPostedFileBase file, int sessionId)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Server.MapPath("/Uploads/SessionMaterials/");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                file.SaveAs(Path.Combine(path, fileName));

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

        [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult UploadSubmission(HttpPostedFileBase file)

        {
            int studentId = (int)Session["UserId"]; //use session for student login to save and use for redirction
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string path = Server.MapPath("~/UploadSubmission/StudentSubmissions/");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                file.SaveAs(Path.Combine(path, fileName));

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
                TempData["UploadSuccess"] = "Your assignment was submitted to path: " + Path.Combine(path, fileName);
                return RedirectToAction("Details", "Student", new { id = studentId });

            }
            TempData["UploadError"] = "Please select a file.";
            return RedirectToAction("Details", "Student", new { id = studentId });

        }

    }
}