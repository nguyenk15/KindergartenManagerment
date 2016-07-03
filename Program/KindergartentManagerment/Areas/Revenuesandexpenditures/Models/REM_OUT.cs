using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class REM_OUT
    {
        [Key]
        public int Out_ID { get; set; }
        [Required]
        public string Out_Code { get; set; }
        public Nullable<System.DateTime> Out_Trade_Date { get; set; }
        public Nullable<System.DateTime> Out_Write_Date { get; set; }
        public Nullable<int> STAFF_ID { get; set; }
        public Nullable<bool> Salary { get; set; }
        public Nullable<int> Out_Year { get; set; }
        public Nullable<int> Out_Month { get; set; }
        public Nullable<int> Supplier_ID { get; set; }
        public string Receiver_Name { get; set; }
        public string Receiver_Address { get; set; }
        public string Out_Reason { get; set; }
        public Nullable<decimal> Out_Total { get; set; }
        public Nullable<decimal> Out_Paid { get; set; }
        public Nullable<decimal> Out_Debt { get; set; }
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}