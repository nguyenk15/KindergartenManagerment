using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class SCHOOLYEAR
    {
        [Key]
        public int YEAR_ID { get; set; }
        public string YEAR_NAME { get; set; }
        public Nullable<int> YEAR_BEGIN { get; set; }
        public Nullable<int> YEAR_END { get; set; }
    }
}