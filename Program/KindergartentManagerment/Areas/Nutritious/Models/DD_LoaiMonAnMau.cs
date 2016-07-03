using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class DD_LoaiMonAnMau
    {
        [Key]
        public int LoaiMonAnMauID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Ten loai mon an mau cannot be longer than 50 characters.")]
        public string TenLoaiMonAnMau { get; set; }
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}