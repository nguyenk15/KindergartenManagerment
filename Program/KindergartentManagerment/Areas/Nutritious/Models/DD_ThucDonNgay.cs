using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class DD_ThucDonNgay
    {
        [Key]
        public int ThucDon_ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> NgayThucDon { get; set; }

        [StringLength(50, ErrorMessage = "Bua sang cannot be longer than 50 characters.")]
        public string BuaSang { get; set; }

        [StringLength(50, ErrorMessage = "Mon man cannot be longer than 50 characters.")]
        public string MonMan { get; set; }

        [StringLength(50, ErrorMessage = "Bua sang cannot be longer than 50 characters.")]
        public string MonCanh { get; set; }

        [StringLength(50, ErrorMessage = "Trang mieng cannot be longer than 50 characters.")]
        public string TrangMieng { get; set; }

        [StringLength(50, ErrorMessage = "Bua sang cannot be longer than 50 characters.")]
        public string BuaXe { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
        public string Notes { get; set; }
    }
}