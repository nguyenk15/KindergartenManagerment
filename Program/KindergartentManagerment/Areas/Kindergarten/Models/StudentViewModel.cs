using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class StudentViewModel
    {
        public GM_CLASSINFO CLASSINFOModel { get; set; }
        public KM_STUDENTOVERVIEW STUDENTOVERVIEWModel { get; set; }
        public int StudentId
        {
            get
            {
                return this.STUDENTOVERVIEWModel.STUDENT_ID;
            }
            set
            {
                this.STUDENTOVERVIEWModel.STUDENT_ID = value;
            }
        }
        public string StudentName
        {
            get
            {
                return this.STUDENTOVERVIEWModel.StudentName;
            }
            set
            {
                this.STUDENTOVERVIEWModel.StudentName = value;
            }
        }
        public string Class
        {
            get
            {
                return this.CLASSINFOModel.Class_Name;
            }
            set
            {
                this.CLASSINFOModel.Class_Name = value;
            }
        }
        public DateTime Birthday
        {
            get
            {
                return this.STUDENTOVERVIEWModel.Date_Of_Birth.HasValue ? this.STUDENTOVERVIEWModel.Date_Of_Birth.Value : DateTime.Now;
            }
            set
            {
                this.STUDENTOVERVIEWModel.Date_Of_Birth = value;
            }
        }
        public string Address
        {
            get
            {
                return this.STUDENTOVERVIEWModel.Address;
            }
            set
            {
                this.STUDENTOVERVIEWModel.Address = value;
            }
        }
        public string Nation
        {
            get
            {
                return this.STUDENTOVERVIEWModel.Nation;
            }
            set
            {
                this.STUDENTOVERVIEWModel.Nation = value;
            }
        }
        public string Picture
        {
            get
            {
                return this.STUDENTOVERVIEWModel.Picture;
            }
            set
            {
                this.STUDENTOVERVIEWModel.Picture = value;
            }
        }
        public string AuthStatus
        {
            get
            {
                return this.STUDENTOVERVIEWModel.Auth_Status;
            }
            set
            {
                this.STUDENTOVERVIEWModel.Auth_Status = value;
            }
        }
        public string Gender
        {
            get
            {
                return this.STUDENTOVERVIEWModel.Gender;
            }
            set
            {
                this.STUDENTOVERVIEWModel.Gender = value;
            }
        }
        public StudentViewModel()
        {
            this.CLASSINFOModel = new GM_CLASSINFO();
            this.STUDENTOVERVIEWModel = new KM_STUDENTOVERVIEW();
        }
    }
}