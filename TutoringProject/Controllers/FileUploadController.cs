using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoringProject.Models;
using TutoringProject.Models.Session;
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

        public ActionResult MaterialGetUploadedFiles(int sessionId)
        {
            using (var db = new TutorContext())
            {
                var files = db.SessionMaterials.Where(f => f.SessionId == sessionId).ToList();
                return PartialView("_MaterialGetUploadedFiles", files);
            }
        }

        public ActionResult MaterialDownloadFile(int id)
        {
            using (var db = new TutorContext())
            {
                var file = db.SessionMaterials.FirstOrDefault(f => f.Id == id);

                if (file == null)
                {
                    return HttpNotFound("File not found.");
                }

                var filePath = Server.MapPath("~/Uploads/SessionMaterials/" + file.FileName);
                return File(filePath, "application/octet-stream", file.FileName);
            }
        }

        public ActionResult SubmissionGetUploadedFiles()
        {
            int studentId = (int)Session["UserId"];
            using (var db = new TutorContext())
            {
                var files = db.StudentSubmissions
                              .Where(f => f.StudentId == studentId)
                              .ToList();
                return PartialView("_SubmissionGetUploadedFiles", files);
            }
        }

        public ActionResult SubmissionDownloadFile(int id)
        {
            using (var db = new TutorContext())
            {
                var file = db.StudentSubmissions.FirstOrDefault(f => f.Id == id);

                if (file == null)
                {
                    return HttpNotFound("File not found.");
                }

                var filePath = Server.MapPath("~/UploadSubmission/StudentSubmissions/" + file.FileName);
                return File(filePath, "application/octet-stream", file.FileName);
            }
        }


    }
}