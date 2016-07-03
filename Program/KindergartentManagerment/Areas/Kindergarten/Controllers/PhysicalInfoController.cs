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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KindergartentManagerment.Areas.Kindergarten.Controllers
{
    public class PhysicalInfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Kindergarten/PhysicalInfo
        public ActionResult Index()
        {
            return View(db.KM_PHYSICALINFO.Where(c => c.Record_Status == "1").ToList());
        }

        // GET: Kindergarten/PhysicalInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KM_PHYSICALINFO kM_PHYSICALINFO = db.KM_PHYSICALINFO.Find(id);
            if (kM_PHYSICALINFO == null)
            {
                return HttpNotFound();
            }
            return View(kM_PHYSICALINFO);
        }

        // GET: Kindergarten/PhysicalInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kindergarten/PhysicalInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KM_PHYSICALINFO kM_PHYSICALINFO)
        {
            if (ModelState.IsValid)
            {
                kM_PHYSICALINFO.Record_Status = "U";
                kM_PHYSICALINFO.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                kM_PHYSICALINFO.Create_DT = DateTime.Now;
                kM_PHYSICALINFO.Auth_Status = "U";
                kM_PHYSICALINFO.Checker_ID = "NULL";
                kM_PHYSICALINFO.Approve_DT = DateTime.Now;
                db.KM_PHYSICALINFO.Add(kM_PHYSICALINFO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kM_PHYSICALINFO);
        }

        // GET: Kindergarten/PhysicalInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KM_PHYSICALINFO kM_PHYSICALINFO = db.KM_PHYSICALINFO.Find(id);
            if (kM_PHYSICALINFO == null)
            {
                return HttpNotFound();
            }
            return View(kM_PHYSICALINFO);
        }

        // POST: Kindergarten/PhysicalInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "STUDENT_ID,Height,Weight,BMI,Dermatology,Otolaryngology,Dentomaxillofacial,RespiratorySystem,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT,Notes,Months,Record_ID")] KM_PHYSICALINFO kM_PHYSICALINFO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kM_PHYSICALINFO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kM_PHYSICALINFO);
        }

        // GET: Kindergarten/PhysicalInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KM_PHYSICALINFO kM_PHYSICALINFO = db.KM_PHYSICALINFO.Find(id);
            if (kM_PHYSICALINFO == null)
            {
                return HttpNotFound();
            }
            return View(kM_PHYSICALINFO);
        }

        // POST: Kindergarten/PhysicalInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KM_PHYSICALINFO kM_PHYSICALINFO = db.KM_PHYSICALINFO.Find(id);
            try
            {
                if (kM_PHYSICALINFO.Auth_Status.Equals("U"))
                    db.KM_PHYSICALINFO.Remove(kM_PHYSICALINFO);
                else
                    kM_PHYSICALINFO.Record_Status = "0";
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
            localReport.ReportPath = Server.MapPath("~/Areas/Kindergarten/Reports/PhysicalInfo/Master.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet";
            reportDataSource.Value = db.KM_PHYSICALINFO.ToList();
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
            Response.AddHeader("content-disposition", "attachment; filename=PhysicalInfo-Master." + fileNameExtension);
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
                localReport.ReportPath = Server.MapPath("~/Areas/Kindergarten/Reports/PhysicalInfo/Details.rdlc");

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet";
                reportDataSource.Value = db.KM_PHYSICALINFO.Where(d => d.STUDENT_ID == id);
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
                Response.AddHeader("content-disposition", "attachment; filename=PhysicalInfo-Details." + fileNameExtension);
                return File(renderedBytes, fileNameExtension);
            }

        }
    }
}
