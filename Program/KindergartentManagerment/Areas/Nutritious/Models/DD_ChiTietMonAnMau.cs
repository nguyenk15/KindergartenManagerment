using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class DD_ChiTietMonAnMau
    {
        [Key]
        public int ChiTietMonAnMauID { get; set; }
        public int MonAnMauID { get; set; }
        public int ThucPhamID { get; set; }
        public Nullable<double> KhoiKuongKg { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}