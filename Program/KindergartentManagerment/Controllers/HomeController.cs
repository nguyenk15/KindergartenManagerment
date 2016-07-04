using KindergartentManagerment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KindergartentManagerment.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View("Index2");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult GetCalendarEvents()
        {
            var eventDetails = db.SYS_EVENT.ToList();

            var eventList = from item in eventDetails
                            select new
                            {
                                id = item.EVENT_ID,
                                title = item.EVENT_NAME,
                                start = item.EVENT_START,
                                end = item.EVENT_END,
                                allDay = false,
                                editable = false,
                                color = item.EVENT_STATUS_COLOR
                            };

            return Json(eventList.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}