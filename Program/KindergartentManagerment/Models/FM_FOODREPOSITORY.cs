using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class FM_FOODREPOSITORY
    {
        [Key]
        public int Food_ID { get; set; }
        public string Food_Code { get; set; }
        public string FoodType { get; set; }
        public string FoodName { get; set; }
        public System.DateTime InputDate { get; set; }
        public string Unit { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public string Supplier { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}