using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class SYS_EVENT
    {
        [Key]
        public int EVENT_ID { get; set; }
        public string EVENT_NAME { get; set; }
        public bool ISALLDAY { get; set; }
        public Nullable<System.DateTime> EVENT_START { get; set; }
        public Nullable<System.DateTime> EVENT_END { get; set; }
        public string EVENT_GROUP { get; set; }
        public string EVENT_DETAIL { get; set; }
        public string EVENT_MANAGER { get; set; }
        public string EVENT_STATUS_COLOR { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public Nullable<System.DateTime> APPROVE_DT { get; set; }
    }
}