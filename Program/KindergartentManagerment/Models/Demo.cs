using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class CalendarEventItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool IsAllDayEvent { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string StatusColor { get; set; }
    }
    public class MockWebServiceCallClass
    {
        public enum AppointmentStatus
        {
            [Description("#01DF3A:ENQUIRY")]
            Enquiry = 0,
            [Description("#FF8000:BOOKED")]
            Booked
        }
        public IList<CalendarEventItem> GetCalendarEvents()
        {
            var list = new List<CalendarEventItem>();

            var random = new Random();
            for (int i = -15; i <= 15; i++)
            {
                var start = DateTime.Now.AddDays(i).AddHours(random.Next(-20, 20));
                list.Add
                    (
                        new CalendarEventItem
                        {
                            ID = random.Next(1, 5000),
                            Title = String.Format("Random Title {0}", random.Next(1, 500)),
                            IsAllDayEvent = false,
                            Start = start,
                            End = start.AddHours(random.Next(1, 6)),
                            StatusColor = "#FF8000"
                        }
                    );
            }
            return list;
        }
    }

}