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
using System.IO;
using ImageResizer;

namespace KindergartentManagerment.Areas.Teach.Controllers
{
    public class TopicController : Controller
    {
        string preAuthStatus = null;
        DateTime? preCheckerDT = null;
        string preCheckerID = null;
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Teach/Topic
        public ActionResult Index()
        {
            return View(db.TM_TOPIC.Where(c => c.Record_Status == "1").ToList());
        }

        // GET: Teach/Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TM_TOPIC tM_TOPIC = db.TM_TOPIC.Find(id);
            if (tM_TOPIC == null)
            {
                return HttpNotFound();
            }
            return View(tM_TOPIC);
        }

        // GET: Teach/Topic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teach/Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TM_TOPIC tM_TOPIC, HttpPostedFileBase picture, HttpPostedFileBase attach)
        {
            if (ModelState.IsValid)
            {
                tM_TOPIC.Record_Status = "1";
                tM_TOPIC.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                tM_TOPIC.Create_DT = DateTime.Now;
                tM_TOPIC.Auth_Status = "U";
                tM_TOPIC.Approve_DT = DateTime.Now;
                if (attach != null && attach.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(attach.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/profile/Teach/Topic/" + tM_TOPIC.Topic_ID +"_"), fileName);
                    attach.SaveAs(path);
                    tM_TOPIC.Attach = "/Content/profile/Teach/Topic/" + tM_TOPIC.Topic_ID + "_" + fileName;
                }
                #region ImageforTopic

                if (picture != null)
                {
                    string pic = tM_TOPIC.Topic_ID.ToString();
                    string path = Server.MapPath("//Content/profile/Teach/Topic/") + pic;
                    var versions = new Dictionary<string, string>();
                    //Define the versions to generate

                    versions.Add("_128x128", "maxwidth=128&maxheight=128&format=jpg");
                    // file is uploaded
                    foreach (var suffix in versions.Keys)
                    {
                        picture.InputStream.Seek(0, SeekOrigin.Begin);

                        //Let the image builder add the correct extension based on the output file type

                        ImageBuilder.Current.Build(
                            new ImageJob(
                                picture.InputStream,
                                path + suffix,
                                new Instructions(versions[suffix]),
                                false,
                                true));

                    }
                    tM_TOPIC.Picture = "/Content/profile/Teach/Topic/" + pic + "_128x128" + ".jpg";
                }
                else
                {
                    tM_TOPIC.Picture = "/Content/profile/" + "user_128x128" + ".png";
                }

                #endregion
                db.TM_TOPIC.Add(tM_TOPIC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tM_TOPIC);
        }

        // GET: Teach/Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TM_TOPIC tM_TOPIC = db.TM_TOPIC.Find(id);
            if (tM_TOPIC == null)
            {
                return HttpNotFound();
            }
            preAuthStatus = tM_TOPIC.Auth_Status;
            preCheckerDT = tM_TOPIC.Approve_DT;
            preCheckerID = tM_TOPIC.Checker_ID;
            return View(tM_TOPIC);
        }

        // POST: Teach/Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Topic_ID,TopicName,Notes,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT")] TM_TOPIC tM_TOPIC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tM_TOPIC).State = EntityState.Modified;
                tM_TOPIC.Create_DT = DateTime.Now;
                tM_TOPIC.Create_DT = DateTime.Now;
                tM_TOPIC.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                tM_TOPIC.Auth_Status = preAuthStatus;
                tM_TOPIC.Checker_ID = preCheckerID;
                tM_TOPIC.Approve_DT = preCheckerDT;
                tM_TOPIC.Record_Status = "1";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tM_TOPIC);
        }

        // GET: Teach/Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TM_TOPIC tM_TOPIC = db.TM_TOPIC.Find(id);
            if (tM_TOPIC == null)
            {
                return HttpNotFound();
            }
            return View(tM_TOPIC);
        }

        // POST: Teach/Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TM_TOPIC tM_TOPIC = db.TM_TOPIC.Find(id);
            try
            {
                if (tM_TOPIC.Auth_Status.Equals("U"))
                    db.TM_TOPIC.Remove(tM_TOPIC);
                else
                    tM_TOPIC.Record_Status = "0";
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
            localReport.ReportPath = Server.MapPath("~/Areas/Teach/Reports/Topic/Master.rdlc");

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
            Response.AddHeader("content-disposition", "attachment; filename=Topic-Master." + fileNameExtension);
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
                localReport.ReportPath = Server.MapPath("~/Areas/Teach/Reports/Topic/Details.rdlc");

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet";
                reportDataSource.Value = db.TM_TOPIC.Where(d => d.Topic_ID == id);
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
                Response.AddHeader("content-disposition", "attachment; filename=Topic-Details." + fileNameExtension);
                return File(renderedBytes, fileNameExtension);
            }

        }
    }
}
