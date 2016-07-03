using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class KM_STUDENTOVERVIEW
    {
        [Key]
        public int STUDENT_ID { get; set; }
        [Required]

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string StudentName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }

        [StringLength(20, ErrorMessage = "Priority Subjects cannot be longer than 20 characters.")]
        public string PrioritySubjects { get; set; }

        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "Nation cannot be longer than 20 characters.")]
        public string Nation { get; set; }

        [StringLength(20, ErrorMessage = "Religion cannot be longer than 20 characters.")]
        public string Religion { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string SchoolYear { get; set; }

        [StringLength(50, ErrorMessage = "FatherName cannot be longer than 50 characters.")]
        public string FatherName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FatherBirthday { get; set; }

        [StringLength(50, ErrorMessage = "FatherJob cannot be longer than 50 characters.")]
        public string FatherJob { get; set; }

        [StringLength(50, ErrorMessage = "MotherName cannot be longer than 50 characters.")]
        public string MotherName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> MotherBirthday { get; set; }

        [StringLength(50, ErrorMessage = "MotherJob cannot be longer than 50 characters.")]
        public string MotherJob { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string FatherPhone { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string MotherPhone { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Picture { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateofAdmission { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
        public string Gender { get; set; }

        public virtual GM_CLASSINFO Class { get; set; }
    }
}