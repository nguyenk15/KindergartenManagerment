using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class TM_TOPIC
    {
        [Key]
        public int Topic_ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Topic name cannot be longer than 50 characters.")]
        public string TopicName { get; set; }

        [StringLength(100, ErrorMessage = "Picture cannot be longer than 100 characters.")]
        public string Picture { get; set; }

        [StringLength(100, ErrorMessage = "Attach cannot be longer than 100 characters.")]
        public string Attach { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
        public string Notes { get; set; }

        [StringLength(1)]
        public string Record_Status { get; set; }

        [StringLength(50)]
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }

        [StringLength(1)]
        public string Auth_Status { get; set; }

        [StringLength(50)]
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
    }
}