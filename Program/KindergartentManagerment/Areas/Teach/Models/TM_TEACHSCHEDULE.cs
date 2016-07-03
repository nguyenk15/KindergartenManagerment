using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class TM_TEACHSCHEDULE
    {
        [Key]
        public int ClassID { get; set; }
        public string DateofWeek { get; set; }
        public System.DateTime Date { get; set; }

        [StringLength(1000, ErrorMessage = "Morning lesson cannot be longer than 1000 characters.")]
        public string MorningLesson { get; set; }

        [StringLength(1000, ErrorMessage = "Morning teacher cannot be longer than 1000 characters.")]
        public string MorningTeacher { get; set; }

        [StringLength(1000, ErrorMessage = "Afternoon lesson cannot be longer than 1000 characters.")]
        public string AfternoonLesson { get; set; }

        [StringLength(1000, ErrorMessage = "Afternoon teacher cannot be longer than 1000 characters.")]
        public string AfternoonTeacher { get; set; }
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}