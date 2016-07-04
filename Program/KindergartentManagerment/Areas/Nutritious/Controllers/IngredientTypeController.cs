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

namespace KindergartentManagerment.Areas.Nutritious.Controllers
{
    public class IngredientTypeController : Controller
    {
        string preAuthStatus = null;
        DateTime? preCheckerDT = null;
        string preCheckerID = null;
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Nutritious/IngredientType
        public ActionResult getResult(string ingredienttype = null)
        {
            var TypeList = new List<String>();
            var type = from d in db.DD_NhomThucPham
                       where d.Record_Status == "1"
                       where d.Auth_Status == "A"
                       orderby d.TenNhomThucPham
                       select d.TenNhomThucPham;
            TypeList.AddRange(type.Distinct());
            ViewBag._type = TypeList;
            IQueryable<DD_NhomThucPham> result = db.DD_NhomThucPham.Where(c => c.Record_Status == "1"
            && (ingredienttype == null || ingredienttype == "" || c.TenNhomThucPham.Contains(ingredienttype))).OrderBy(c => c.TenNhomThucPham);
            return View(result.ToList());
        }
        public ActionResult Index(string ingredienttype = null)
        {
            return getResult(ingredienttype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? id, string AUTH_STATUS, string ingredienttype = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DD_NhomThucPham dD_NhomThucPham = db.DD_NhomThucPham.Find(id);
            if (dD_NhomThucPham == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(dD_NhomThucPham).State = EntityState.Modified;
                dD_NhomThucPham.Auth_Status = AUTH_STATUS;
                dD_NhomThucPham.Checker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                dD_NhomThucPham.Approve_DT = DateTime.Now;
                db.SaveChanges();
            }
            return getResult(ingredienttype);
        }
        // GET: Nutritious/Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DD_NhomThucPham a = db.DD_NhomThucPham.Find(id);
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
        public void AllViewBag()
        {
            var TypeList = new List<String>();
            var type = from d in db.DD_NhomThucPham
                       where d.Record_Status == "1"
                       where d.Auth_Status == "A"
                       orderby d.TenNhomThucPham
                       select d.TenNhomThucPham;
            TypeList.AddRange(type.Distinct());
            ViewBag._type = TypeList;
        }
        // POST: Kindergarten/StudentOverview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DD_NhomThucPham DD_NhomThucPhamModel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DD_NhomThucPhamModel.Record_Status = "1";
                    DD_NhomThucPhamModel.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                    DD_NhomThucPhamModel.Create_DT = DateTime.Now;
                    DD_NhomThucPhamModel.Auth_Status = "U";
                    DD_NhomThucPhamModel.Checker_ID = null;
                    DD_NhomThucPhamModel.Approve_DT = DateTime.Now;
                    db.DD_NhomThucPham.Add(DD_NhomThucPhamModel);
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

            return View(DD_NhomThucPhamModel);
        }
        // GET: Kindergarten/StudentOverview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DD_NhomThucPham a = db.DD_NhomThucPham.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            preAuthStatus = a.Auth_Status;
            preCheckerDT = a.Approve_DT;
            preCheckerID = a.Checker_ID;
            return View(a);
        }

        // POST: Kindergarten/StudentOverview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DD_NhomThucPham nhomthucpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhomthucpham).State = EntityState.Modified;
                nhomthucpham.Create_DT = DateTime.Now;
                nhomthucpham.Create_DT = DateTime.Now;
                nhomthucpham.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                nhomthucpham.Auth_Status = preAuthStatus;
                nhomthucpham.Checker_ID = preCheckerID;
                nhomthucpham.Approve_DT = preCheckerDT;
                nhomthucpham.Record_Status = "1";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhomthucpham);
        }
        // GET: Kindergarten/StudentOverview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DD_NhomThucPham a = db.DD_NhomThucPham.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        // POST: Kindergarten/StudentOverview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DD_NhomThucPham a = db.DD_NhomThucPham.Find(id);
            if (a.Auth_Status.Equals("U"))
                db.DD_NhomThucPham.Remove(a);
            else
                a.Record_Status = "0";
            db.SaveChanges();
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