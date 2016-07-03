using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KindergartentManagerment.Models;
using KindergartentManagerment.Resources;
using System.IO;
using ImageResizer;
using System.Diagnostics;
using System.Data.Entity.Validation;
using Microsoft.Reporting.WebForms;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Resources;
using System.Collections;
using System.Globalization;

namespace KindergartentManagerment.Areas.Kindergarten.Controllers
{
    public class StudentOverviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        public ActionResult getResult(string sortOrder, int? classid, string studentname = null,
                   string gender = null)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var grade = db.GM_CLASSINFO.Where(c => c.Record_Status == "1" && c.Auth_Status == "A").OrderBy(c => c.Class_Name).ToList();
            ViewBag._ClassList = new SelectList(grade, "ClassID", "Class_Name", classid);
            int Class_ID = classid.GetValueOrDefault();
            IQueryable<KM_STUDENTOVERVIEW> result = db.KM_STUDENTOVERVIEW.
                Where(c => (!classid.HasValue || c.ClassID == classid)
                && (studentname == null || c.StudentName.Contains(studentname))
                && (gender == "2" || c.Gender.Equals(gender))
                && c.Record_Status == "1").
                OrderBy(c => c.ClassID).ThenBy(c => c.StudentName).
                Include(c => c.Class);
            switch(sortOrder)
            {
                case "name_desc":
                    result = result.OrderBy(s => s.Class.Class_Name).ThenByDescending(s => s.StudentName);
                    break;
                case "Date":
                    result = result.OrderBy(s => s.Class.Class_Name).ThenBy(s => s.Date_Of_Birth);
                    break;
                case "date_desc":
                    result = result.OrderBy(s => s.Class.Class_Name).ThenByDescending(s => s.Date_Of_Birth);
                    break;
                default:
                    result = result.OrderBy(s => s.Class.Class_Name).ThenBy(s => s.StudentName);
                    break;
            }
            return View(result.ToList());
        }
        public ActionResult Index(string sortOrder, int? classid, string studentname = null,
            string gender = null)
        {
            return getResult(sortOrder, classid, studentname, gender);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string sortOrder, int? id, string AUTH_STATUS,
            int? classid, string studentname = null,
            string gender = null)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            KM_STUDENTOVERVIEW kM_STUDENTOVERVIEW = db.KM_STUDENTOVERVIEW.Find(id);
            if (kM_STUDENTOVERVIEW == null)
                return HttpNotFound();
            if (ModelState.IsValid)
            {
                db.Entry(kM_STUDENTOVERVIEW).State = EntityState.Modified;
                kM_STUDENTOVERVIEW.Auth_Status = AUTH_STATUS;
                kM_STUDENTOVERVIEW.Checker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                kM_STUDENTOVERVIEW.Approve_DT = DateTime.Now;
                db.SaveChanges();
            }
            return getResult(sortOrder, classid, studentname, gender);
        }


        // GET: Kindergarten/StudentOverview/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KM_STUDENTOVERVIEW kM_STUDENTOVERVIEW = db.KM_STUDENTOVERVIEW.Find(id);
            if (kM_STUDENTOVERVIEW == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(kM_STUDENTOVERVIEW);
        }

        // GET: Kindergarten/StudentOverview/Create
        public void AllViewBag() {
            List<SelectListItem> listNation = new List<SelectListItem>();
            ResourceSet resourceSet = NationCommonResource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet)
            {
                listNation.Add(new SelectListItem { Text = item.Value.ToString(), Value = item.Key.ToString() });
            }
            ViewBag.Nation = listNation;
            ViewBag.Class = new SelectList(db.GM_CLASSINFO.Where(s => s.Auth_Status == "A"), "ClassID", "Class_Name");
            ViewBag.SchoolYear = new SelectList(db.SCHOOLYEARs, "YEAR_ID", "YEAR_NAME");
            List<SelectListItem> listGender = new List<SelectListItem>();
            listGender.Add(new SelectListItem { Text = StudentOverviewResource.Female, Value = "0" });
            listGender.Add(new SelectListItem { Text = StudentOverviewResource.Male, Value = "1" });
            ViewBag.Gender = listGender;
        }
        public ActionResult Create()
        {
            AllViewBag();
            return View();
        }

        // POST: Kindergarten/StudentOverview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KM_STUDENTOVERVIEW kM_STUDENTOVERVIEW, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    kM_STUDENTOVERVIEW.Record_Status = "1";
                    kM_STUDENTOVERVIEW.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                    kM_STUDENTOVERVIEW.Create_DT = DateTime.Now;
                    kM_STUDENTOVERVIEW.Auth_Status = "U";
                    kM_STUDENTOVERVIEW.Checker_ID = null;
                    kM_STUDENTOVERVIEW.Approve_DT = DateTime.Now;
                    #region ImageforKindergartent

                    if (file != null)
                    {
                        string pic = kM_STUDENTOVERVIEW.STUDENT_ID.ToString();
                        string path = Server.MapPath("//Content/profile/Kindergarten/") + pic;
                        var versions = new Dictionary<string, string>();
                        //Define the versions to generate

                        versions.Add("_128x128", "maxwidth=128&maxheight=128&format=jpg");
                        versions.Add("_160x160", "maxwidth=128&maxheight=128&format=jpg");
                        // file is uploaded
                        foreach (var suffix in versions.Keys)
                        {
                            file.InputStream.Seek(0, SeekOrigin.Begin);

                            //Let the image builder add the correct extension based on the output file type

                            ImageBuilder.Current.Build(
                                new ImageJob(
                                    file.InputStream,
                                    path + suffix,
                                    new Instructions(versions[suffix]),
                                    false,
                                    true));

                        }
                        kM_STUDENTOVERVIEW.Picture = "/Content/profile/kindergarten/" + pic + "_128x128" + ".jpg";


                    }
                    else
                    {
                        kM_STUDENTOVERVIEW.Picture = "/Content/profile/" + "user_128x128" + ".png";

                    }

                    #endregion
                    if (kM_STUDENTOVERVIEW.ClassID != null)
                        kM_STUDENTOVERVIEW.Class.Quantity++;
                    db.KM_STUDENTOVERVIEW.Add(kM_STUDENTOVERVIEW);
                    //Increase class's quantity
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.
                        PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            AllViewBag();
            return View(kM_STUDENTOVERVIEW);
        }

        // GET: Kindergarten/StudentOverview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KM_STUDENTOVERVIEW kM_STUDENTOVERVIEW = db.KM_STUDENTOVERVIEW.Find(id);
            if (kM_STUDENTOVERVIEW == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(kM_STUDENTOVERVIEW);
        }

        // POST: Kindergarten/StudentOverview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KM_STUDENTOVERVIEW kM_STUDENTOVERVIEW, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (kM_STUDENTOVERVIEW.ClassID != null)
                    kM_STUDENTOVERVIEW.Class.Quantity--;
                db.Entry(kM_STUDENTOVERVIEW).State = EntityState.Modified;
                kM_STUDENTOVERVIEW.Create_DT = DateTime.Now;
                #region ImageforKindergartent
                if (file != null)
                {
                    string pic = kM_STUDENTOVERVIEW.STUDENT_ID.ToString();
                    string path = Server.MapPath("//Content/profile/Kindergarten/") + pic;
                    var versions = new Dictionary<string, string>();
                    //Define the versions to generate

                    versions.Add("edit_128x128", "maxwidth=128&maxheight=128&format=jpg");
                    versions.Add("edit_160x160", "maxwidth=128&maxheight=128&format=jpg");
                    // file is uploaded
                    foreach (var suffix in versions.Keys)
                    {
                        file.InputStream.Seek(0, SeekOrigin.Begin);

                        //Let the image builder add the correct extension based on the output file type

                        ImageBuilder.Current.Build(
                            new ImageJob(
                                file.InputStream,
                                path + suffix,
                                new Instructions(versions[suffix]),
                                false,
                                true));

                    }
                    kM_STUDENTOVERVIEW.Picture = "/Content/profile/kindergarten/" + pic + "edit_128x128" + ".jpg";


                }
                else
                {
                    kM_STUDENTOVERVIEW.Picture = "/Content/profile/" + "user_128x128" + ".png";

                }

                #endregion
                if (kM_STUDENTOVERVIEW.ClassID != null)
                    kM_STUDENTOVERVIEW.Class.Quantity++;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            AllViewBag();
            return View(kM_STUDENTOVERVIEW);
        }

        // GET: Kindergarten/StudentOverview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KM_STUDENTOVERVIEW kM_STUDENTOVERVIEW = db.KM_STUDENTOVERVIEW.Find(id);
            if (kM_STUDENTOVERVIEW == null)
            {
                return HttpNotFound();
            }
            //Increase class's quantity
            if (kM_STUDENTOVERVIEW.ClassID != null)
                kM_STUDENTOVERVIEW.Class.Quantity--;
            var content = (from p in db.GM_CLASSINFO
                           where p.ClassID == kM_STUDENTOVERVIEW.ClassID
                           select p.Class_Name).FirstOrDefault();
            ViewBag.Text = content;
            return View(kM_STUDENTOVERVIEW);
        }

        // POST: Kindergarten/StudentOverview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KM_STUDENTOVERVIEW kM_STUDENTOVERVIEW = db.KM_STUDENTOVERVIEW.Find(id);
            try {
                if (kM_STUDENTOVERVIEW.Auth_Status.Equals("U"))
                    db.KM_STUDENTOVERVIEW.Remove(kM_STUDENTOVERVIEW);
                else
                    kM_STUDENTOVERVIEW.Record_Status = "0";
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

        public FileResult ExportTo(string ReportType, string studentname = null, string classname = null, string gender = null)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Areas/Kindergarten/Reports/StudentOverview/Master.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet";
            reportDataSource.Value = db.KM_STUDENTOVERVIEW.ToList();
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
            Response.AddHeader("content-disposition", "attachment; filename=StudentOverview-Master." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);
        }
        public FileResult DetailsExportTo(int? id,string ReportType)
        {
            if (id == null)
            {
                return File("xyz","abc");
            }
            else
            {

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Areas/Kindergarten/Reports/StudentOverview/Details.rdlc");
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet";
                reportDataSource.Value = db.KM_STUDENTOVERVIEW.Where(d => d.STUDENT_ID == id);
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
                Response.AddHeader("content-disposition", "attachment; filename=StudentOverview-Details." + fileNameExtension);
                return File(renderedBytes, fileNameExtension);
            }
            
        }
    }
}
