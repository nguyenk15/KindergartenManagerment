using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class StaffViewModel
    {
        public SM_STAFFINFO STAFFINFOModel { get; set; }
        public DM_DEPARTMENTINFO DEPARTMENTINFOModel { get; set; }
        public string AuthStatus
        {
            get
            {
                return STAFFINFOModel.Auth_Status;
            }
            set
            {
                STAFFINFOModel.Auth_Status = value;
            }
        }
        public int StaffId
        {
            get
            {
                return STAFFINFOModel.STAFF_ID;
            }
            set
            {
                STAFFINFOModel.STAFF_ID = value;
            }
        }
        public string StaffName
        {
            get
            {
                return STAFFINFOModel.StaffName;
            }
            set
            {
                STAFFINFOModel.StaffName = value;
            }
        }
        public string Gender
        {
            get
            {
                return STAFFINFOModel.Gender;
            }
            set
            {
                STAFFINFOModel.Gender = value;
            }
        }
        public string Position
        {
            get
            {
                return STAFFINFOModel.Position;
            }
            set
            {
                STAFFINFOModel.Position = value;
            }
        }
        public string Department
        {
            get
            {
                return DEPARTMENTINFOModel.DepartmentName;
            }
            set
            {
                DEPARTMENTINFOModel.DepartmentName = value;
            }
        }
        public DateTime Birthday
        {
            get
            {
                return STAFFINFOModel.Date_Of_Birth.HasValue ? STAFFINFOModel.Date_Of_Birth.Value : DateTime.Now;
            }
            set
            {
                STAFFINFOModel.Date_Of_Birth = value;
            }
        }
        //public bool IsATeacher
        //{
        //    get
        //    {
        //        return STAFFINFOModel.IsATeacher;//.HasValue ? STAFFINFOModel.Date_Of_Birth.Value : DateTime.Now;
        //    }
        //    set
        //    {
        //        STAFFINFOModel.IsATeacher = value;
        //    }
        //}
        public string Picture
        {
            get
            {
                return STAFFINFOModel.Picture;
            }
            set
            {
                STAFFINFOModel.Picture = value;
            }
        }
        public StaffViewModel()
        {
            STAFFINFOModel = new SM_STAFFINFO();
            DEPARTMENTINFOModel = new DM_DEPARTMENTINFO();
        }
    }
}