using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class SM_STAFFINFO
    {
        [Key]
        public int STAFF_ID { get; set; }
        [Required]
        public string StaffNumber { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Staff name cannot be longer than 50 characters.")]
        public string StaffName { get; set; }
        [Required]
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }

        [StringLength(20, ErrorMessage = "Nation cannot be longer than 20 characters.")]
        public string Nation { get; set; }

        [StringLength(20, ErrorMessage = "Religion cannot be longer than 20 characters.")]
        public string Religion { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "IndentityCard cannot be longer than 50 characters.")]
        public string IndentityCard { get; set; }

        [StringLength(50, ErrorMessage = "IssuedBy cannot be longer than 50 characters.")]
        public string IssuedBy { get; set; }
        public Nullable<System.DateTime> DateRange { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Position cannot be longer than 50 characters.")]
        public string Position { get; set; }

        [StringLength(50, ErrorMessage = "Degree cannot be longer than 50 characters.")]
        public string Degree { get; set; }
        public Nullable<decimal> SalaryGrade { get; set; }
        public int? DepartmentID { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<decimal> CoefficientsSalary { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<decimal> BasicSalary { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<decimal> DayWage { get; set; }
        public Nullable<System.DateTime> WageIncreaseDay { get; set; }
        public string FatherFullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FatherBirthday { get; set; }
        public string FatherJob { get; set; }
        public string MotherFullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> MotherBirthday { get; set; }
        public string MotherJob { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string FatherPhone { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string MotherPhone { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Picture { get; set; }
        public bool IsLeaguer { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DayatParty { get; set; }
        public string CardNumberParty { get; set; }
        public string WorkingStatus { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> StartWorkDay { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Dayoff { get; set; }
        public string TypeofContract { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual DM_DEPARTMENTINFO Department { get; set; }
        //[InverseProperty("DM_DEPARTMENTINFO")] // <- Navigation property name in EntityA
        //public virtual ICollection<DM_DEPARTMENTINFO> DM_DEPARTMENTINFOs { get; set; }
        //public virtual SYS_AUTH_STATUS AuthStatus { get; set; }
    }
}