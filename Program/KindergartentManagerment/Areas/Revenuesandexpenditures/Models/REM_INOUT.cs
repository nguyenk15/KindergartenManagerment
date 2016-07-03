using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class REM_INOUT
    {
        [Key]
        public int InOut_ID { get; set; }
        public string InOut_Code { get; set; }
        public Nullable<System.DateTime> InOut_Date { get; set; }
        public string InOut_Reason { get; set; }
        public Nullable<decimal> InOut_Received { get; set; }
        public Nullable<decimal> InOut_Paid { get; set; }
        public Nullable<decimal> InOut_Budget { get; set; }
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}