using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindergartentManagerment.Models
{
    public class KM_PHYSICALINFO
    {
        [Key]
        public int RECORD_ID { get; set; }
        public Nullable<decimal> Height { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> BMI { get; set; }

        public int STUDENT_ID { get; set; }

        [StringLength(100, ErrorMessage = "Dermatology cannot be longer than 100 characters.")]
        public string Dermatology { get; set; }

        [StringLength(100, ErrorMessage = "Otolaryngology cannot be longer than 100 characters.")]
        public string Otolaryngology { get; set; }

        [StringLength(100, ErrorMessage = "Dentomaxillofacial cannot be longer than 100 characters.")]
        public string Dentomaxillofacial { get; set; }

        [StringLength(100, ErrorMessage = "Respiratory System cannot be longer than 100 characters.")]
        public string RespiratorySystem { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
        public string Notes { get; set; }
        public Nullable<int> Months { get; set; }

        [ForeignKey("STUDENT_ID")]
        public virtual KM_STUDENTOVERVIEW Student { get; set; }
    }
}