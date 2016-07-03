using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class REM_OUT_DETAIL
    {
        [Key]
        public int Out_Detail_ID { get; set; }
        public int Out_ID { get; set; }
        public string Product_Name { get; set; }
        public string Unit { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}