using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class DD_MonAnMau
    {
        [Key]
        public int MonAnMauID { get; set; }
        public int LoaiMonAnMauID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Ten mon an nau cannot be longer than 50 characters.")]
        public string TenMonAnMau { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
        public string Notes { get; set; }

        [StringLength(50, ErrorMessage = "Record_Status cannot be longer than 50 characters.")]
        public string Record_Status { get; set; }

        [StringLength(50, ErrorMessage = "Maker_ID cannot be longer than 50 characters.")]
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}