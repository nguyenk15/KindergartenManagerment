using KindergartentManagerment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KindergartentManagerment.Areas.GradeClass.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Kindergarten/Calendar
        KindergartentManagerment.Models.ApplicationDbContext db = new KindergartentManagerment.Models.ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///     API: http://arshaw.com/fullcalendar/docs/event_data/Event_Object/
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SYS_EVENT item)
        {
            if (ModelState.IsValid)
            {

                db.SYS_EVENT.Add(item);
                db.SaveChanges();
            }
            return View();
        }
    }
}