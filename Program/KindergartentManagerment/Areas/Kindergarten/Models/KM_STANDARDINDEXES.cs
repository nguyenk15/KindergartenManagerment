using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class KM_STANDARDINDEXES
    {
        [Key]
        public int Record_ID { get; set; }
        public int Months { get; set; }
        public Nullable<int> StandardHeight { get; set; }

        [StringLength(100, ErrorMessage = "HeightUnit cannot be longer than 100 characters.")]
        public string HeightUnit { get; set; }

        public Nullable<int> StandardWeight { get; set; }

        [StringLength(100, ErrorMessage = "WeightUnit cannot be longer than 100 characters.")]
        public string WeightUnit { get; set; }

        public Nullable<decimal> StandardBMI { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot be longer than 100 characters.")]
        public string Notes { get; set; }
    }
}