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
    public class OutController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        // GET: Revenuesandexpenditures/Out
        public ActionResult Index()
        {
            return View(db.REM_OUT.ToList());
        }

        // GET: Revenuesandexpenditures/Out/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REM_OUT rEM_OUT = db.REM_OUT.Find(id);
            if (rEM_OUT == null)
            {
                return HttpNotFound();
            }
            return View(rEM_OUT);
        }

        // GET: Revenuesandexpenditures/Out/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Revenuesandexpenditures/Out/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Out_ID,Out_Code,Out_Trade_Date,Out_Write_Date,STAFF_ID,Salary,Out_Year,Out_Month,Supplier_ID,Receiver_Name,Receiver_Address,Out_Reason,Out_Total,Out_Paid,Out_Debt,Notes,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT")] REM_OUT rEM_OUT)
        {
            if (ModelState.IsValid)
            {
                rEM_OUT.Record_Status = "U";
                rEM_OUT.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                rEM_OUT.Create_DT = DateTime.Now;
                rEM_OUT.Auth_Status = "U";
                rEM_OUT.Checker_ID = "";
                rEM_OUT.Approve_DT = DateTime.Now;
                db.REM_OUT.Add(rEM_OUT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rEM_OUT);
        }

        // GET: Revenuesandexpenditures/Out/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REM_OUT rEM_OUT = db.REM_OUT.Find(id);
            if (rEM_OUT == null)
            {
                return HttpNotFound();
            }
            return View(rEM_OUT);
        }

        // POST: Revenuesandexpenditures/Out/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Out_ID,Out_Code,Out_Trade_Date,Out_Write_Date,STAFF_ID,Salary,Out_Year,Out_Month,Supplier_ID,Receiver_Name,Receiver_Address,Out_Reason,Out_Total,Out_Paid,Out_Debt,Notes,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT")] REM_OUT rEM_OUT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEM_OUT).State = EntityState.Modified;
                rEM_OUT.Create_DT = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rEM_OUT);
        }

        // GET: Revenuesandexpenditures/Out/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REM_OUT rEM_OUT = db.REM_OUT.Find(id);
            if (rEM_OUT == null)
            {
                return HttpNotFound();
            }
            return View(rEM_OUT);
        }

        // POST: Revenuesandexpenditures/Out/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REM_OUT rEM_OUT = db.REM_OUT.Find(id);
            try
            {
                if (rEM_OUT.Auth_Status.Equals("U"))
                    db.REM_OUT.Remove(rEM_OUT);
                else
                    rEM_OUT.Record_Status = "0";
                db.SaveChanges();
            }
            catch (Exception ex)
            {
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
    }
}
