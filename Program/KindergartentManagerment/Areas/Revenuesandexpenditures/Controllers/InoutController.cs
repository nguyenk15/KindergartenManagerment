using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KindergartentManagerment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KindergartentManagerment.Areas.Revenuesandexpenditures.Controllers
{
    public class InoutController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        // GET: Revenuesandexpenditures/Inout
        public ActionResult Index()
        {
            return View(db.REM_INOUT.ToList());
        }

        // GET: Revenuesandexpenditures/Inout/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REM_INOUT rEM_INOUT = db.REM_INOUT.Find(id);
            if (rEM_INOUT == null)
            {
                return HttpNotFound();
            }
            return View(rEM_INOUT);
        }

        // GET: Revenuesandexpenditures/Inout/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Revenuesandexpenditures/Inout/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InOut_ID,InOut_Code,InOut_Date,InOut_Reason,InOut_Received,InOut_Paid,InOut_Budget,Notes,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT")] REM_INOUT rEM_INOUT)
        {
            if (ModelState.IsValid)
            {
                rEM_INOUT.Record_Status = "U";
                rEM_INOUT.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                rEM_INOUT.Create_DT = DateTime.Now;
                rEM_INOUT.Auth_Status = "U";
                rEM_INOUT.Checker_ID = "NULL";
                rEM_INOUT.Approve_DT = DateTime.Now;
                db.REM_INOUT.Add(rEM_INOUT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rEM_INOUT);
        }

        // GET: Revenuesandexpenditures/Inout/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REM_INOUT rEM_INOUT = db.REM_INOUT.Find(id);
            if (rEM_INOUT == null)
            {
                return HttpNotFound();
            }
            return View(rEM_INOUT);
        }

        // POST: Revenuesandexpenditures/Inout/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InOut_ID,InOut_Code,InOut_Date,InOut_Reason,InOut_Received,InOut_Paid,InOut_Budget,Notes,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT")] REM_INOUT rEM_INOUT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEM_INOUT).State = EntityState.Modified;
                rEM_INOUT.Create_DT = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rEM_INOUT);
        }

        // GET: Revenuesandexpenditures/Inout/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REM_INOUT rEM_INOUT = db.REM_INOUT.Find(id);
            if (rEM_INOUT == null)
            {
                return HttpNotFound();
            }
            return View(rEM_INOUT);
        }

        // POST: Revenuesandexpenditures/Inout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REM_INOUT rEM_INOUT = db.REM_INOUT.Find(id);
            try
            {
                if (rEM_INOUT.Auth_Status.Equals("U"))
                    db.REM_INOUT.Remove(rEM_INOUT);
                else
                    rEM_INOUT.Record_Status = "0";
                db.SaveChanges();
            }
            catch (Exception e)
            { }
            
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
