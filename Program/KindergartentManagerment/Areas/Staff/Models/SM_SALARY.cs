using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class SM_SALARY
    {
        [Key]
        public int Sa_ID { get; set; }
        public int STAFF_ID { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<decimal> Sa_Total { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<decimal> Sa_Bonus { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<decimal> Sa_Minus { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<decimal> Sa_Salary { get; set; }
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }

        public virtual SM_STAFFINFO Staff { get; set; }
        //public virtual SYS_AUTH_STATUS AuthStatus { get; set; }
    }
}