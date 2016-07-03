using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KindergartentManagerment.Models;
using Microsoft.Reporting.WebForms;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.IO;

namespace KindergartentManagerment.Areas.Teach.Controllers
{
    public class LessonProgramController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Teach/LessonProgram
        public ActionResult Index()
        {
            return View(db.TM_LESSONPROGRAM.Where(c => c.Record_Status == "1").ToList());
        }

        // GET: Teach/LessonProgram/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TM_LESSONPROGRAM tM_LESSONPROGRAM = db.TM_LESSONPROGRAM.Find(id);
            if (tM_LESSONPROGRAM == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(tM_LESSONPROGRAM);
        }

        // GET: Teach/LessonProgram/Create
        public ActionResult Create()
        {
            AllViewBag();
            return View();
        }
        public void AllViewBag()
        {
            var TeacherList = db.SM_STAFFINFO.Where(m => m.Position == "Teacher" && m.Auth_Status == "A").OrderBy(m => m.StaffName).ToList();
            var TopicList = db.TM_TOPIC.Where(m => m.Auth_Status == "A").OrderBy(m => m.TopicName).ToList();
            if (TopicList == null)
                ViewBag.TopicList = null;
            else
                ViewBag.TopicList = new SelectList(TopicList, "Topic_ID", "TopicName");
            if (TeacherList == null)
                ViewBag.TeacherList = null;
            else
                ViewBag.TeacherList = new SelectList(TeacherList, "STAFF_ID", "StaffName");
        }
        // POST: Teach/LessonProgram/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TM_LESSONPROGRAM tM_LESSONPROGRAM, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                tM_LESSONPROGRAM.Record_Status = "U";
                tM_LESSONPROGRAM.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                tM_LESSONPROGRAM.Create_DT = DateTime.Now;
                tM_LESSONPROGRAM.Auth_Status = "U";
                tM_LESSONPROGRAM.Approve_DT = DateTime.Now;
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("/Content/profile/Teach/LessonProgram/" + tM_LESSONPROGRAM.Topic_ID + "_"), fileName);
                    file.SaveAs(path);
                    tM_LESSONPROGRAM.Attach = path;
                }
                db.TM_LESSONPROGRAM.Add(tM_LESSONPROGRAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tM_LESSONPROGRAM);
        }

        // GET: Teach/LessonProgram/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TM_LESSONPROGRAM tM_LESSONPROGRAM = db.TM_LESSONPROGRAM.Find(id);
            if (tM_LESSONPROGRAM == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(tM_LESSONPROGRAM);
        }

        // POST: Teach/LessonProgram/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Topic_ID,Lesson_ID,LessonName,LessonGoal,TeacherActivity,KidActivity,Author,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT,Notes")] TM_LESSONPROGRAM tM_LESSONPROGRAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tM_LESSONPROGRAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tM_LESSONPROGRAM);
        }

        // GET: Teach/LessonProgram/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TM_LESSONPROGRAM tM_LESSONPROGRAM = db.TM_LESSONPROGRAM.Find(id);
            if (tM_LESSONPROGRAM == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(tM_LESSONPROGRAM);
        }

        // POST: Teach/LessonProgram/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TM_LESSONPROGRAM tM_LESSONPROGRAM = db.TM_LESSONPROGRAM.Find(id);
            try
            {
                if (tM_LESSONPROGRAM.Auth_Status.Equals("U"))
                    db.TM_LESSONPROGRAM.Remove(tM_LESSONPROGRAM);
                else
                    tM_LESSONPROGRAM.Record_Status = "0";
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public FileResult ExportTo(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Areas/Teach/Reports/LessonProgram/Master.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet";
            reportDataSource.Value = db.TM_LESSONPROGRAM.ToList();
            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            switch (ReportType)
            {
                case "Excel":
                    fileNameExtension = "xlsx";
                    break;
                case "Pdf":
                    fileNameExtension = "pdf";
                    break;
                case "Word":
                    fileNameExtension = "docx";
                    break;

            }
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding,
                                                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=LessonProgram-Master." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);
        }
        public FileResult DetailsExportTo(int? id, string ReportType)
        {
            if (id == null)
            {
                return File("xyz", "abc");
            }
            else
            {

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Areas/Teach/Reports/LessonProgram/Details.rdlc");

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet";
                reportDataSource.Value = db.TM_LESSONPROGRAM.Where(d => d.Lesson_ID == id);
                localReport.DataSources.Add(reportDataSource);

                string reportType = ReportType;
                string mimeType;
                string encoding;
                string fileNameExtension;
                switch (ReportType)
                {
                    case "Excel":
                        fileNameExtension = "xlsx";
                        break;
                    case "Pdf":
                        fileNameExtension = "pdf";
                        break;
                    case "Word":
                        fileNameExtension = "docx";
                        break;

                }
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding,
                                                    out fileNameExtension, out streams, out warnings);
                Response.AddHeader("content-disposition", "attachment; filename=LessonProgram-Details." + fileNameExtension);
                return File(renderedBytes, fileNameExtension);
            }

        }
    }
}
