using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class UM_ROLEDELIVERY
    {
        [Key]
        public int Group_ID { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
    }
}