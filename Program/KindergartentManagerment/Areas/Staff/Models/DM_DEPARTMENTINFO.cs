using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindergartentManagerment.Models
{
    public class DM_DEPARTMENTINFO
    {
        [Key]
        public int Depatment_ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Department name cannot be longer than 50 characters.")]
        public string DepartmentName { get; set; }
        public Nullable<int> Chief_ID { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
        public string Notes { get; set; }
        //[ForeignKey("Chief_ID")]
        //public virtual SM_STAFFINFO Chief { get; set; }
       
        //public virtual SYS_AUTH_STATUS AuthStatus { get; set; }
    }
}