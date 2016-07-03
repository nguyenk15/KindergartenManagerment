using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class DD_NhuCauNangLuong
    {
        [Key]
        public int DDNhuCauNangLuongID { get; set; }

        [StringLength(2, ErrorMessage = "Do tuoi cannot be longer than 2 characters.")]
        public string DoTuoi { get; set; }
        public Nullable<double> CaloCaNgay { get; set; }
        public Nullable<double> CaloTruong { get; set; }
        public Nullable<double> ProtidCaNgay { get; set; }
        public Nullable<double> ProtidTruong { get; set; }
        public Nullable<double> LipidCaNgay { get; set; }
        public Nullable<double> LipidTruong { get; set; }
        public Nullable<double> GlucidCaNgay { get; set; }
        public Nullable<double> GlucidTruong { get; set; }

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