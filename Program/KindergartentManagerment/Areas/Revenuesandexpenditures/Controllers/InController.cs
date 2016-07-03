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

namespace KindergartentManagerment.Areas.Revenuesandexpenditures.Controllers
{
    public class InController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Revenuesandexpenditures/In
        public ActionResult Index()
        {
            return View(db.REM_IN.ToList());
        }

        // GET: Revenuesandexpenditures/In/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REM_IN rEM_IN = db.REM_IN.Find(id);
            if (rEM_IN == null)
            {
                return HttpNotFound();
            }
            return View(rEM_IN);
        }

        // GET: Revenuesandexpenditures/In/Create
        public ActionResult Create(string CusType)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Bé", Value = "0" });
            list.Add(new SelectListItem { Text = "Nhân Viên", Value = "1" });
            ViewBag.CusList = new SelectList(list, "value", "text");
            switch (CusType)
            {
                case "0":
                    ViewBag.CusSelect = new SelectList(db.KM_STUDENTOVERVIEW.ToList(), "STUDENT_ID", "StudentName");
                    break;
                case "1":
                    ViewBag.CusSelect = new SelectList(db.SM_STAFFINFO.ToList(), "STAFF_ID", "StaffName");
                    break;
                default:
                    ViewBag.CusSelect = new SelectList(db.KM_STUDENTOVERVIEW.ToList(), "STUDENT_ID", "StudentName");
                    break;
            }
            return View();
        }

        // POST: Revenuesandexpenditures/In/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REM_IN rEM_IN,string CusType)
        {
            switch (CusType)
            {
                case "0":
                    ViewBag.CusSelect = new SelectList(db.KM_STUDENTOVERVIEW.ToList(), "STUDENT_ID", "StudentName");
                    break;
                case "1":
                    ViewBag.CusSelect = new SelectList(db.SM_STAFFINFO.ToList(), "STAFF_ID", "StaffName");
                    break;
            }
            if (ModelState.IsValid)
            {
                rEM_IN.Record_Status = "U";
                rEM_IN.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                rEM_IN.Create_DT = DateTime.Now;
                rEM_IN.Auth_Status = "U";
                rEM_IN.Checker_ID = "";
                rEM_IN.Approve_DT = DateTime.Now;
                db.REM_IN.Add(rEM_IN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rEM_IN);
        }

        // GET: Revenuesandexpenditures/In/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REM_IN rEM_IN = db.REM_IN.Find(id);
            if (rEM_IN == null)
            {
                return HttpNotFound();
            }
            return View(rEM_IN);
        }

        // POST: Revenuesandexpenditures/In/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "In_ID,In_Code,Date_Trade_In,Customer_ID,School_Fee,Year,Month,Payer_Name,Payer_Address,In_Reason,In_Total,In_Received,In_Debt,Attach,In_Status,Notes,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT")] REM_IN rEM_IN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEM_IN).State = EntityState.Modified;
                rEM_IN.Create_DT = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rEM_IN);
        }

        // GET: Revenuesandexpenditures/In/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REM_IN rEM_IN = db.REM_IN.Find(id);
            if (rEM_IN == null)
            {
                return HttpNotFound();
            }
            return View(rEM_IN);
        }

        // POST: Revenuesandexpenditures/In/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REM_IN rEM_IN = db.REM_IN.Find(id);
            try
            {
                if (rEM_IN.Auth_Status.Equals("U"))
                    db.REM_IN.Remove(rEM_IN);
                else
                    rEM_IN.Record_Status = "0";
                db.SaveChanges();
            }
            catch (Exception ex) {

            }
            
            
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
            localReport.ReportPath = Server.MapPath("~/Areas/Revenuesandexpenditures/Reports/In/Master.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet";
            reportDataSource.Value = db.REM_IN.ToList();
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
            Response.AddHeader("content-disposition", "attachment; filename=In-Master." + fileNameExtension);
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
                localReport.ReportPath = Server.MapPath("~/Areas/Revenuesandexpenditures/Reports/In/Details.rdlc");

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet";
                reportDataSource.Value = db.REM_IN.Where(d => d.In_ID == id);
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
                Response.AddHeader("content-disposition", "attachment; filename=In-Details." + fileNameExtension);
                return File(renderedBytes, fileNameExtension);
            }

        }
    }
}
