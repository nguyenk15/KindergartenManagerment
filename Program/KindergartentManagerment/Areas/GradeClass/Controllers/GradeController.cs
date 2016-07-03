using KindergartentManagerment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KindergartentManagerment.Areas.GradeClass.Controllers
{
    public class GradeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: GradeClass/Grade
        public ActionResult Index()
        {
            IQueryable<GM_GRADEINFO> result = db.GM_GRADEINFO.Where(c => c.Record_Status == "1").OrderBy(c => c.GRADE_NAME);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? id, string AUTH_STATUS)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_GRADEINFO gM_GRADEINFO = db.GM_GRADEINFO.Find(id);
            if (gM_GRADEINFO == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(gM_GRADEINFO).State = EntityState.Modified;
                gM_GRADEINFO.Auth_Status = AUTH_STATUS;
                gM_GRADEINFO.Checker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                gM_GRADEINFO.Approve_DT = DateTime.Now;
                db.SaveChanges();
            }
            IQueryable<GM_GRADEINFO> result = db.GM_GRADEINFO.Where(c => c.Record_Status == "1").OrderBy(c => c.GRADE_NAME);
            return View(result);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_GRADEINFO gM_GRADEINFO = db.GM_GRADEINFO.Find(id);
            if (gM_GRADEINFO == null)
            {
                return HttpNotFound();
            }
            return View(gM_GRADEINFO);
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kindergarten/StudentOverview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GM_GRADEINFO gM_GRADEINFO, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    gM_GRADEINFO.Record_Status = "1";
                    gM_GRADEINFO.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                    gM_GRADEINFO.Create_DT = DateTime.Now;
                    gM_GRADEINFO.Auth_Status = "U";
                    gM_GRADEINFO.Checker_ID = null;
                    gM_GRADEINFO.Approve_DT = DateTime.Now;
                    db.GM_GRADEINFO.Add(gM_GRADEINFO);
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

            return View(gM_GRADEINFO);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_GRADEINFO gM_GRADEINFO = db.GM_GRADEINFO.Find(id);
            if (gM_GRADEINFO == null)
            {
                return HttpNotFound();
            }
            //db.SaveChanges();
            return View(gM_GRADEINFO);
        }

        // POST: Kindergarten/StudentOverview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GM_GRADEINFO gM_GRADEINFO, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gM_GRADEINFO).State = EntityState.Modified;
                gM_GRADEINFO.Create_DT = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gM_GRADEINFO);
        }
        // GET: Kindergarten/StudentOverview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_GRADEINFO gM_GRADEINFO = db.GM_GRADEINFO.Find(id);
            if (gM_GRADEINFO == null)
            {
                return HttpNotFound();
            }
            return View(gM_GRADEINFO);
        }

        // POST: Kindergarten/StudentOverview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GM_GRADEINFO gM_GRADEINFO = db.GM_GRADEINFO.Find(id);
            try
            {
                if (gM_GRADEINFO.Auth_Status.Equals("U"))
                    db.GM_GRADEINFO.Remove(gM_GRADEINFO);
                else
                    gM_GRADEINFO.Record_Status = "0";
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
    }
}