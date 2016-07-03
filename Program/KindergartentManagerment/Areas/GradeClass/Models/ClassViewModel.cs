using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class ClassViewModel
    {
        public GM_CLASSINFO GM_CLASSINFOModel { get; set; }
        public GM_GRADEINFO GM_GRADEINFOModel { get; set; }
        public SM_STAFFINFO TeacherModel { get; set; }
        public SM_STAFFINFO KindergartenerModel { get; set; }
        public string AuthStatus
        {
            get
            {
                return GM_CLASSINFOModel.Auth_Status;//.HasValue ? DD_ThucPhamModel.VitaminPP.Value : 0;
            }
            set
            {
                GM_CLASSINFOModel.Auth_Status = value;
            }
        }
        public int ClassID
        {
            get
            {
                return GM_CLASSINFOModel.ClassID;
            }
            set
            {
                GM_CLASSINFOModel.ClassID = value;
            }
        }
        public string ClassName {
            get
            {
                return GM_CLASSINFOModel.Class_Name;
            }
            set
            {
                GM_CLASSINFOModel.Class_Name = value;
            }
        }
        public string TeacherName
        {
            get
            {
                return TeacherModel.StaffName;
            }
            set
            {
                TeacherModel.StaffName = value;
            }
        }
        public string KindergartenerName
        {
            get
            {
                return KindergartenerModel.StaffName;
            }
            set
            {
                KindergartenerModel.StaffName = value;
            }
        }
        public string GradeName
        {
            get
            {
                return GM_GRADEINFOModel.GRADE_NAME;
            }
            set
            {
                GM_GRADEINFOModel.GRADE_NAME = value;
            }
        }
        public int Quantity
        {
            get
            {
                return GM_CLASSINFOModel.Quantity;
            }
            set
            {
                GM_CLASSINFOModel.Quantity = value;
            }
        }
        public ClassViewModel()
        {
            GM_CLASSINFOModel = new GM_CLASSINFO();
            GM_GRADEINFOModel = new GM_GRADEINFO();
            TeacherModel = new SM_STAFFINFO();
            KindergartenerModel = new SM_STAFFINFO();
        }
    }
}