using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class SYS_AUTH_STATUS
    {
        [Key]
		[StringLength(1)]
        public string AUTH_STATUS { get; set; }
		[StringLength(20)]
        public string AUTH_STATUS_NAME { get; set; }
    }
}