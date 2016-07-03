using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class DD_NhomThucPham
    {
        [Key]
        public int NhomThucPhamID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "TenNhomThucPham cannot be longer than 50 characters.")]
        public string TenNhomThucPham { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }

        //public virtual SYS_AUTH_STATUS AuthStatus { get; set; }
    }
}