using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class UM_USERINFO
    {
        [Key]
        public int Group_ID { get; set; }
        public int User_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        public string Gender { get; set; }
        public string Job { get; set; }
    }
}