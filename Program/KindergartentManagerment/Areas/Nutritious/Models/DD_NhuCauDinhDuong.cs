using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class DD_NhuCauDinhDuong
    {
        [Key]
        public int DDNhuCauDinhDuongID { get; set; }
        public int Tuoi { get; set; }
        public Nullable<double> Kcalo { get; set; }
        public Nullable<double> Protein { get; set; }
        public Nullable<double> Calsi { get; set; }
        public Nullable<double> Sat { get; set; }
        public Nullable<double> VitaminA { get; set; }
        public Nullable<double> VitaminB1 { get; set; }
        public Nullable<double> VitaminB2 { get; set; }
        public Nullable<double> VitaminPP { get; set; }
        public Nullable<double> VitaminC { get; set; }

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