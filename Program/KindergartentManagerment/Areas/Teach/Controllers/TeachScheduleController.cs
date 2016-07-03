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

namespace KindergartentManagerment.Areas.Teach.Controllers
{
    public class TeachScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Teach/TeachSchedule
        public ActionResult Index()
        {
            return View(db.TM_TEACHSCHEDULE.Where(c => c.Record_Status == "1").ToList());
        }

        // GET: Teach/TeachSchedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TM_TEACHSCHEDULE tM_TEACHSCHEDULE = db.TM_TEACHSCHEDULE.Find(id);
            if (tM_TEACHSCHEDULE == null)
            {
                return HttpNotFound();
            }
            return View(tM_TEACHSCHEDULE);
        }

        // GET: Teach/TeachSchedule/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Teach/TeachSchedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TM_TEACHSCHEDULE tM_TEACHSCHEDULE)
        {
            if (ModelState.IsValid)
            {
                tM_TEACHSCHEDULE.Record_Status = "U";
                tM_TEACHSCHEDULE.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                tM_TEACHSCHEDULE.Create_DT = DateTime.Now;
                tM_TEACHSCHEDULE.Auth_Status = "U";
                tM_TEACHSCHEDULE.Checker_ID = "NULL";
                tM_TEACHSCHEDULE.Approve_DT = DateTime.Now;
                db.TM_TEACHSCHEDULE.Add(tM_TEACHSCHEDULE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tM_TEACHSCHEDULE);
        }

        // GET: Teach/TeachSchedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TM_TEACHSCHEDULE tM_TEACHSCHEDULE = db.TM_TEACHSCHEDULE.Find(id);
            if (tM_TEACHSCHEDULE == null)
            {
                return HttpNotFound();
            }
            return View(tM_TEACHSCHEDULE);
        }

        // POST: Teach/TeachSchedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,DateofWeek,Date,MorningLesson,MorningTeacher,AfternoonLesson,AfternoonTeacher,Notes,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT")] TM_TEACHSCHEDULE tM_TEACHSCHEDULE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tM_TEACHSCHEDULE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tM_TEACHSCHEDULE);
        }

        // GET: Teach/TeachSchedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TM_TEACHSCHEDULE tM_TEACHSCHEDULE = db.TM_TEACHSCHEDULE.Find(id);
            if (tM_TEACHSCHEDULE == null)
            {
                return HttpNotFound();
            }
            return View(tM_TEACHSCHEDULE);
        }

        // POST: Teach/TeachSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TM_TEACHSCHEDULE tM_TEACHSCHEDULE = db.TM_TEACHSCHEDULE.Find(id);
            try
            {
                if (tM_TEACHSCHEDULE.Auth_Status.Equals("U"))
                    db.TM_TEACHSCHEDULE.Remove(tM_TEACHSCHEDULE);
                else
                    tM_TEACHSCHEDULE.Record_Status = "0";
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
            localReport.ReportPath = Server.MapPath("~/Areas/Teach/Reports/TeachSchedule/Master.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet";
            reportDataSource.Value = db.TM_TEACHSCHEDULE.ToList();
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
            Response.AddHeader("content-disposition", "attachment; filename=TeachSchedule-Master." + fileNameExtension);
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
                localReport.ReportPath = Server.MapPath("~/Areas/Teach/Reports/TeachSchedule/Details.rdlc");

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet";
                reportDataSource.Value = db.TM_TEACHSCHEDULE.Where(d => d.ClassID == id);
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
                Response.AddHeader("content-disposition", "attachment; filename=TeachSchedule-Details." + fileNameExtension);
                return File(renderedBytes, fileNameExtension);
            }

        }
    }
}
