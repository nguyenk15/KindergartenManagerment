using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using KindergartentManagerment.Resources;
using System.Web.Mvc;

namespace KindergartentManagerment.Models
{
    public class REM_IN
    {
        [Key]
        public int In_ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The string cannot be longer than 50 characters.")]
        public string In_Code { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date_Trade_In { get; set; }

        [Required]
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<bool> School_Fee { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }

        [StringLength(50, ErrorMessage = "The string cannot be longer than 50 characters.")]
        public string Payer_Name { get; set; }
        public string Payer_Address { get; set; }
        public string In_Reason { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float? In_Total { get; set; }

        [DataType(DataType.Currency)]
        public float? In_Received { get; set; }

        [DataType(DataType.Currency)]
        public float? In_Debt { get; set; }
        public string Attach { get; set; }
        public Nullable<bool> In_Status { get; set; }
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
    
    
}