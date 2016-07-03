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
    public class IngredientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Nutritious/Ingredients
        public ActionResult getResult(int? nhomthucpham, string tenthucpham = null)
        {
            var foodType = db.DD_NhomThucPham.Where(c => c.Record_Status == "1" && c.Auth_Status == "A")
                .OrderBy(q => q.TenNhomThucPham).ToList();
            ViewBag.nhomthucpham = new SelectList(foodType, "NhomThucPhamID", "TenNhomThucPham", nhomthucpham);
            int NhomThucPhamID = nhomthucpham.GetValueOrDefault();
            Debug.WriteLine(NhomThucPhamID);
            IQueryable<DD_ThucPham> RESULTS = db.DD_ThucPham
                .Where(c => (tenthucpham == null || c.TenThucPham.Contains(tenthucpham))
                && (!nhomthucpham.HasValue || c.NhomThucPhamID == NhomThucPhamID)
                && c.Record_Status == "1")
                .OrderBy(s => s.NhomThucPhamID)
                .ThenBy(s => s.TenThucPham)
                .Include(s => s.NhomThucPham);
            return View(RESULTS.ToList());
        }
        public ActionResult Index(int? nhomthucpham, string tenthucpham = null)
        {
            return getResult(nhomthucpham, tenthucpham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? id, string AUTH_STATUS, int? nhomthucpham, string tenthucpham = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DD_ThucPham dD_ThucPham = db.DD_ThucPham.Find(id);
            if (dD_ThucPham == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(dD_ThucPham).State = EntityState.Modified;
                dD_ThucPham.Auth_Status = AUTH_STATUS;
                dD_ThucPham.Checker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                dD_ThucPham.Approve_DT = DateTime.Now;
                db.SaveChanges();
            }
            return getResult(nhomthucpham, tenthucpham);
        }
        // GET: Nutritious/Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DD_ThucPham a = db.DD_ThucPham.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            var content = (from p in db.DD_NhomThucPham where p.NhomThucPhamID == a.NhomThucPhamID
                          select p.TenNhomThucPham).FirstOrDefault();
            ViewBag.Text = content;
            return View(a);
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
        public ActionResult Create(DD_ThucPham DD_ThucPhamModel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DD_ThucPhamModel.Record_Status = "1";
                    DD_ThucPhamModel.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                    DD_ThucPhamModel.Create_DT = DateTime.Now;
                    DD_ThucPhamModel.Auth_Status = "U";
                    DD_ThucPhamModel.Checker_ID = null;
                    DD_ThucPhamModel.Approve_DT = DateTime.Now;
                    db.DD_ThucPham.Add(DD_ThucPhamModel);
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

            return View(DD_ThucPhamModel);
        }

        // GET: Kindergarten/StudentOverview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DD_ThucPham a = db.DD_ThucPham.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            //db.SaveChanges();
            return View(a);
        }

        // POST: Kindergarten/StudentOverview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DD_ThucPham thucpham, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thucpham).State = EntityState.Modified;
                thucpham.Create_DT = DateTime.Now;
                //var content = from p in db.DD_NhomThucPham
                //              join b in db.DD_ThucPham
                //              on p.NhomThucPhamID equals b.NhomThucPhamID
                //              select new { b.NhomThucPhamID, p.TenNhomThucPham };
                //ViewBag.GroupList = content
                //       .Select(c => new SelectListItem
                //       {
                //           Text = c.TenNhomThucPham,
                //           Value = c.NhomThucPhamID.ToString()
                //       }).ToList();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thucpham);
        }

        // GET: Kindergarten/StudentOverview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DD_ThucPham a = db.DD_ThucPham.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            var content = (from p in db.DD_NhomThucPham
                           where p.NhomThucPhamID == a.NhomThucPhamID
                           select p.TenNhomThucPham).FirstOrDefault();
            ViewBag.Text = content;
            return View(a);
        }

        // POST: Kindergarten/StudentOverview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DD_ThucPham dD_ThucPham = db.DD_ThucPham.Find(id);
            if (dD_ThucPham.Auth_Status.Equals("U"))
                db.DD_ThucPham.Remove(dD_ThucPham);
            else
                dD_ThucPham.Record_Status = "0";
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