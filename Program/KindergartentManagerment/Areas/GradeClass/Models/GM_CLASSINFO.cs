using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindergartentManagerment.Models
{
    public class GM_CLASSINFO
    {
        [Key]
        public int ClassID { get; set; }
        public int? Teacher_ID { get; set; }
        public int GradeID { get; set; }
        [StringLength(100, ErrorMessage = "Accountability cannot be longer than 100 characters.")]
        public string Accountability { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
        public string Notes { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Class Name cannot be longer than 50 characters.")]
        public string Class_Name { get; set; }
        public int Quantity { get; set; }
        public int? Kindergartener_ID { get; set; }
        public int? SchoolYear_ID { get; set; }

        [ForeignKey("Teacher_ID")]
        public virtual SM_STAFFINFO Teacher { get; set; }
        public virtual GM_GRADEINFO Grade { get; set; }
        [ForeignKey("Kindergartener_ID")]
        public virtual SM_STAFFINFO Kindergartener { get; set; }
        //[ForeignKey("SchoolYear_ID")]
        //public virtual SCHOOLYEAR SchoolYear{ get; set; }

    }
}