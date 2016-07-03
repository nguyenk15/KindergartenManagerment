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

namespace KindergartentManagerment.Areas.Staff.Controllers
{
    public class SalaryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Staff/Salary
        public ActionResult Index()
        {
            return View(db.SM_SALARY.Where(c => c.Record_Status == "1").ToList());
        }

        // GET: Staff/Salary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SM_SALARY sM_SALARY = db.SM_SALARY.Find(id);
            if (sM_SALARY == null)
            {
                return HttpNotFound();
            }
            return View(sM_SALARY);
        }

        // GET: Staff/Salary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/Salary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SM_SALARY sM_SALARY)
        {
            if (ModelState.IsValid)
            {
                sM_SALARY.Record_Status = "1";
                sM_SALARY.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                sM_SALARY.Create_DT = DateTime.Now;
                sM_SALARY.Auth_Status = "U";
                sM_SALARY.Checker_ID = null;
                sM_SALARY.Approve_DT = DateTime.Now;
                db.SM_SALARY.Add(sM_SALARY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sM_SALARY);
        }

        // GET: Staff/Salary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SM_SALARY sM_SALARY = db.SM_SALARY.Find(id);
            if (sM_SALARY == null)
            {
                return HttpNotFound();
            }
            return View(sM_SALARY);
        }

        // POST: Staff/Salary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sa_ID,Sa_StaffCode,Sa_Total,Sa_Bonus,Sa_Minus,Sa_Salary,Notes,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT")] SM_SALARY sM_SALARY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sM_SALARY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sM_SALARY);
        }

        // GET: Staff/Salary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SM_SALARY sM_SALARY = db.SM_SALARY.Find(id);
            if (sM_SALARY == null)
            {
                return HttpNotFound();
            }
            return View(sM_SALARY);
        }

        // POST: Staff/Salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SM_SALARY sM_SALARY = db.SM_SALARY.Find(id);
            try
            {
                if (sM_SALARY.Auth_Status.Equals("U"))
                    db.SM_SALARY.Remove(sM_SALARY);
                else
                    sM_SALARY.Record_Status = "0";
                db.SaveChanges();
            }
            catch ( Exception ex)
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
