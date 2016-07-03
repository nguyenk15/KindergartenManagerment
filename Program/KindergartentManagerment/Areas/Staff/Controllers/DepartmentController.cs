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

namespace KindergartentManagerment.Areas.Staff.Controllers
{
    public class DepartmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Staff/Department
        public ActionResult Index()
        {
            IQueryable<DM_DEPARTMENTINFO> result = db.DM_DEPARTMENTINFO.Where(c => c.Record_Status == "1").OrderBy(c => c.DepartmentName);
            return View(result.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? id, string AUTH_STATUS)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_DEPARTMENTINFO dM_DEPARTMENTINFO = db.DM_DEPARTMENTINFO.Find(id);
            if (dM_DEPARTMENTINFO == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(dM_DEPARTMENTINFO).State = EntityState.Modified;
                dM_DEPARTMENTINFO.Auth_Status = AUTH_STATUS;
                dM_DEPARTMENTINFO.Checker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                dM_DEPARTMENTINFO.Approve_DT = DateTime.Now;
                db.SaveChanges();
            }
            IQueryable<DM_DEPARTMENTINFO> result = db.DM_DEPARTMENTINFO.OrderBy(c => c.DepartmentName);
            return View(result.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_DEPARTMENTINFO a = db.DM_DEPARTMENTINFO.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DM_DEPARTMENTINFO DM_DEPARTMENTINFOModel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DM_DEPARTMENTINFOModel.Record_Status = "1";
                    DM_DEPARTMENTINFOModel.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                    DM_DEPARTMENTINFOModel.Create_DT = DateTime.Now;
                    DM_DEPARTMENTINFOModel.Auth_Status = "U";
                    DM_DEPARTMENTINFOModel.Checker_ID = null;
                    DM_DEPARTMENTINFOModel.Approve_DT = DateTime.Now;
                    db.DM_DEPARTMENTINFO.Add(DM_DEPARTMENTINFOModel);
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

            return View(DM_DEPARTMENTINFOModel);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_DEPARTMENTINFO a = db.DM_DEPARTMENTINFO.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            //db.SaveChanges();
            return View(a);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DM_DEPARTMENTINFO DM_DEPARTMENTINFOModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(DM_DEPARTMENTINFOModel).State = EntityState.Modified;
                DM_DEPARTMENTINFOModel.Create_DT = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DM_DEPARTMENTINFOModel);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_DEPARTMENTINFO a = db.DM_DEPARTMENTINFO.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DM_DEPARTMENTINFO a = db.DM_DEPARTMENTINFO.Find(id);
            try
            {
                if (a.Auth_Status.Equals("U"))
                    db.DM_DEPARTMENTINFO.Remove(a);
                else
                    a.Record_Status = "0";
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