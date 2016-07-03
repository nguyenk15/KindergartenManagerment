using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class SYS_GROUP
    {
        [Key]
        public string GROUP_ID { get; set; }
        public string GROUP_CODE { get; set; }
        public string GROUP_NAME { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public Nullable<System.DateTime> APPROVE_DT { get; set; }
    }
}