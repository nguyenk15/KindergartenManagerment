using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class DD_SuatAn
    {
        [Key]
        public int DDSuatAnID { get; set; }
        public int DDLoaiSuatAnID { get; set; }
        public Nullable<int> SoLuongSuatAn { get; set; }

        [StringLength(50, ErrorMessage = "TenSuatAn cannot be longer than 50 characters.")]
        public string TenSuatAn { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Ngay { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<double> TongTienSuatAn { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}