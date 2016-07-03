using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using KindergartentManagerment.Models;
using KindergartentManagerment.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using System.IO;
using ImageResizer;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace KindergartentManagerment.Areas.Staff.Controllers
{
    public class StaffInfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Staff/SM_STAFFINFO
        public ActionResult getResult(int? departmentid, string staffname = null,
     string gender = null,
     string position = null)
        {
            List<SelectListItem> listPosition = new List<SelectListItem>();
            ResourceSet resourceSet_2 = PositionCommonResource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet_2)
            {
                listPosition.Add(new SelectListItem { Text = item.Value.ToString(), Value = item.Key.ToString() });
            }
            ViewBag.Position = new SelectList(listPosition, "Value", "Text", position);
            var dep = db.DM_DEPARTMENTINFO.Where(c => c.Record_Status == "1" && c.Auth_Status == "A").OrderBy(c => c.DepartmentName).ToList();
            ViewBag._department = new SelectList(dep, "Depatment_ID", "DepartmentName", departmentid);
            int Department_ID = departmentid.GetValueOrDefault();
            IQueryable<SM_STAFFINFO> result = db.SM_STAFFINFO.
                Where(c => (!departmentid.HasValue || c.DepartmentID == Department_ID)
                && (staffname == null || c.StaffName.Contains(staffname))
                && (gender == "2" || c.Gender.Equals(gender))
                && (position == null || c.Position.Contains(position))
                && c.Record_Status == "1").
                OrderBy(c => c.DepartmentID).ThenBy(c => c.StaffName).
                Include(c => c.Department);
            return View(result.ToList());
        }
        public ActionResult Index(int? departmentid, string staffname = null,
            string gender = null,
            string isateacher = null,
            string isakindergartener = null,
            string position = null)
        {
            return getResult(departmentid, staffname, gender, position);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? departmentid, int? id, string AUTH_STATUS,
           string staffname = null,
            string gender = null,
            string position = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SM_STAFFINFO sM_STAFFINFO = db.SM_STAFFINFO.Find(id);
            if (sM_STAFFINFO == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(sM_STAFFINFO).State = EntityState.Modified;
                sM_STAFFINFO.Auth_Status = AUTH_STATUS;
                sM_STAFFINFO.Checker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                sM_STAFFINFO.Approve_DT = DateTime.Now;
                db.SaveChanges();
            }
            return getResult(departmentid, staffname, gender, position);
        }
        // GET: Staff/SM_STAFFINFO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SM_STAFFINFO sM_STAFFINFO = db.SM_STAFFINFO.Find(id);
            if (sM_STAFFINFO == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(sM_STAFFINFO);
        }
        public void AllViewBag()
        {
            List<SelectListItem> listNation = new List<SelectListItem>();
            ResourceSet resourceSet = NationCommonResource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet)
            {
                listNation.Add(new SelectListItem { Text = item.Value.ToString(), Value = item.Key.ToString()});
            }
            //listNation.Sort();
            ViewBag.Nation = listNation;
            List<SelectListItem> listPosition = new List<SelectListItem>();
            ResourceSet resourceSet_2 = PositionCommonResource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet_2)
            {
                listPosition.Add(new SelectListItem { Text = item.Value.ToString(), Value = item.Key.ToString() });
            }
            ViewBag.Position = listPosition;
            ViewBag.DepartmentList = new SelectList(db.DM_DEPARTMENTINFO.Where(s => s.Auth_Status == "A"), "Depatment_ID", "DepartmentName");
            List<SelectListItem> listGender = new List<SelectListItem>();
            listGender.Add(new SelectListItem { Text = StudentOverviewResource.Female, Value = "0" });
            listGender.Add(new SelectListItem { Text = StudentOverviewResource.Male, Value = "1" });
            ViewBag.Gender = listGender;
        }
        // GET: Staff/SM_STAFFINFO/Create
        public ActionResult Create()
        {
            AllViewBag();
            return View();
        }

        // POST: Staff/SM_STAFFINFO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SM_STAFFINFO sM_STAFFINFO, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sM_STAFFINFO.Record_Status = "1";
                    sM_STAFFINFO.Maker_ID = userManager.FindById(User.Identity.GetUserId()).Id;
                    sM_STAFFINFO.Create_DT = DateTime.Now;
                    sM_STAFFINFO.Auth_Status = "U";
                    sM_STAFFINFO.Checker_ID = null;
                    sM_STAFFINFO.Approve_DT = DateTime.Now;
                    #region ImageforStaff

                    if (file != null)
                    {
                        string pic = sM_STAFFINFO.STAFF_ID.ToString();
                        string path = Server.MapPath("//Content/profile/Staff/") + pic;
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
                        sM_STAFFINFO.Picture = "/Content/profile/kindergarten/" + pic + "_128x128" + ".jpg";


                    }
                    else
                    {
                        sM_STAFFINFO.Picture = "/Content/profile/" + "user_128x128" + ".png";

                    }

                    #endregion
                    db.SM_STAFFINFO.Add(sM_STAFFINFO);
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
            return View(sM_STAFFINFO);
        }
        // GET: Staff/SM_STAFFINFO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SM_STAFFINFO sM_STAFFINFO = db.SM_STAFFINFO.Find(id);
            if (sM_STAFFINFO == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(sM_STAFFINFO);
        }

        // POST: Staff/SM_STAFFINFO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "STAFF_ID,StaffNumber,StaffName,Gender,Date_Of_Birth,Nation,Religion,Address,IsATeacher,IndentityCard,IssuedBy,DateRange,Phone,Email,Position,Degree,SalaryGrade,CoefficientsSalary,BasicSalary,WagesDay,DayWageIncress,FatherisFullName,BirthFather,JobFather,MotherisFullName,BirthMother,JobMother,TelephoneFather,TelephoneMother,Picture,IsLeaguer,DayatParty,CardNumberParty,WorkingStatus,DayStartedMaking,Dayoff,TypeofContract,Record_Status,Maker_ID,Create_DT,Auth_Status,Checker_ID,Approve_DT")] SM_STAFFINFO sM_STAFFINFO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sM_STAFFINFO).State = EntityState.Modified;
                sM_STAFFINFO.Create_DT = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            AllViewBag();
            return View(sM_STAFFINFO);
        }

        // GET: Staff/SM_STAFFINFO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SM_STAFFINFO sM_STAFFINFO = db.SM_STAFFINFO.Find(id);
            if (sM_STAFFINFO == null)
            {
                return HttpNotFound();
            }
            AllViewBag();
            return View(sM_STAFFINFO);
        }

        // POST: Staff/SM_STAFFINFO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SM_STAFFINFO sM_STAFFINFO = db.SM_STAFFINFO.Find(id);
            try
            {
                if (sM_STAFFINFO.Auth_Status.Equals("U"))
                    db.SM_STAFFINFO.Remove(sM_STAFFINFO);
                else
                    sM_STAFFINFO.Record_Status = "0";
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
