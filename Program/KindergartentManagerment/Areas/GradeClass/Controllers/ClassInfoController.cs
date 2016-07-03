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
    public class ClassInfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: GradeClass/ClassInfo
        public ActionResult getResult(int? khoilop,
           int? siso, string giaovien = null,
           string baomau = null, string tenlop = null)
        {
            var grade = db.GM_GRADEINFO.Where(c => c.Auth_Status == "A" && c.Record_Status == "1").OrderBy(c => c.GRADE_NAME).ToList();
            ViewBag._GradeList = new SelectList(grade, "GradeID", "GRADE_NAME", khoilop);
            int gradeID = khoilop.GetValueOrDefault();
            var GVList = new List<string>();

            var gvl = from d in db.SM_STAFFINFO
                     where d.Position == "Teacher"
                     where d.Record_Status == "1"
                     where d.Auth_Status == "A"
                     orderby d.StaffName
                     select d.StaffName;
            GVList.AddRange(gvl.Distinct());
            ViewBag._giaovien = GVList;
            var BMList = new List<string>();

            var bml = from d in db.SM_STAFFINFO
                     where d.Position == "Kindergartener"
                     where d.Record_Status == "1"
                     where d.Auth_Status == "A"
                     orderby d.StaffName
                     select d.StaffName;
            BMList.AddRange(bml.Distinct());
            ViewBag._baomau = BMList;
            int giaovienID = 0, baomauID = 0;
            if (giaovien != null)
            {
                var gv = db.SM_STAFFINFO.Where(c => c.StaffName.Equals(giaovien)).FirstOrDefault();
                if (gv != null)
                    giaovienID = gv.STAFF_ID;
            }
            if (baomau != null)
            {
                var bm = db.SM_STAFFINFO.Where(c => c.StaffName.Equals(giaovien)).FirstOrDefault();
                if (bm != null)
                    baomauID = bm.STAFF_ID;
            }
            IQueryable<GM_CLASSINFO> result = db.GM_CLASSINFO
               .Where(c => (!khoilop.HasValue || c.GradeID == gradeID)
               && (giaovienID == 0 || c.Teacher_ID == giaovienID)
               && (baomauID == 0 || c.Kindergartener_ID == baomauID)
               && (tenlop == null || c.Class_Name.Contains(tenlop))
               && (!siso.HasValue || c.Quantity == siso)
               && c.Record_Status == "1")
               .OrderBy(c => c.GradeID).ThenBy(c => c.ClassID)
               .Include(c => c.Grade)
               .Include(c => c.Teacher)
               .Include(c => c.Kindergartener);
            return View(result.ToList());
        }
        public ActionResult Index(int? khoilop,
            int? siso, string giaovien = null,
            string baomau = null, string tenlop = null)
        {
            return getResult(khoilop, siso, giaovien, baomau, tenlop);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? id, string AUTH_STATUS, int? khoilop,
            int? siso, string giaovien = null,
            string baomau = null, string tenlop = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_CLASSINFO gM_CLASSINFO = db.GM_CLASSINFO.Find(id);
            if (gM_CLASSINFO == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(gM_CLASSINFO).State = EntityState.Modified;
                gM_CLASSINFO.Auth_Status = AUTH_STATUS;
                gM_CLASSINFO.Checker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                gM_CLASSINFO.Approve_DT = DateTime.Now;
                db.SaveChanges();
            }
            return getResult(khoilop, siso, giaovien, baomau, tenlop);
        }
        // GET: Nutritious/Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_CLASSINFO a = db.GM_CLASSINFO.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(a);
        }
        public void AllViewBag()
        {
            ViewBag.GradeList = new SelectList(db.GM_GRADEINFO.Where(s => s.Auth_Status == "A"), "GradeID", "GRADE_NAME");
            ViewBag.TeacherList = new SelectList(db.SM_STAFFINFO.Where(s => s.Position == "Teacher" && s.Auth_Status == "A")
                                                   , "STAFF_ID", "StaffName");
            ViewBag.KindergartenerList = new SelectList(db.SM_STAFFINFO.Where(s => s.Position == "Kindergartener" && s.Auth_Status == "A")
                                                   , "STAFF_ID", "StaffName");
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
        public ActionResult Create(GM_CLASSINFO GM_CLASSINFOModel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GM_CLASSINFOModel.Record_Status = "1";
                    GM_CLASSINFOModel.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                    GM_CLASSINFOModel.Create_DT = DateTime.Now;
                    GM_CLASSINFOModel.Auth_Status = "U";
                    GM_CLASSINFOModel.Checker_ID = null;
                    GM_CLASSINFOModel.Approve_DT = DateTime.Now;
                    GM_CLASSINFOModel.Quantity = 0;
                    db.GM_CLASSINFO.Add(GM_CLASSINFOModel);
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

            return View(GM_CLASSINFOModel);
        }
        // GET: Kindergarten/StudentOverview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_CLASSINFO a = db.GM_CLASSINFO.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(a);
        }

        // POST: Kindergarten/StudentOverview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GM_CLASSINFO GM_CLASSINFOModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(GM_CLASSINFOModel).State = EntityState.Modified;
                GM_CLASSINFOModel.Create_DT = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            AllViewBag();
            return View(GM_CLASSINFOModel);
        }

        // GET: Kindergarten/StudentOverview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_CLASSINFO a = db.GM_CLASSINFO.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(a);
        }

        // POST: Kindergarten/StudentOverview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GM_CLASSINFO a = db.GM_CLASSINFO.Find(id);
            try
            {
                if (a.Auth_Status.Equals("U"))
                    db.GM_CLASSINFO.Remove(a);
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