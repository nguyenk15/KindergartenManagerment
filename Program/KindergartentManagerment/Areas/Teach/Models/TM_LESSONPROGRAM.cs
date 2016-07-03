using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class TM_LESSONPROGRAM
    {
        [Key]
        public int Lesson_ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Lesson name cannot be longer than 50 characters.")]
        public string LessonName { get; set; }

        [Required]
        public int Topic_ID { get; set; }

        [StringLength(1000, ErrorMessage = "Lesson goal cannot be longer than 1000 characters.")]
        public string LessonGoal { get; set; }

        [StringLength(1000, ErrorMessage = "TeacherActivity cannot be longer than 1000 characters.")]
        public string TeacherActivity { get; set; }

        [StringLength(1000, ErrorMessage = "Kid activity cannot be longer than 1000 characters.")]
        public string KidActivity { get; set; }

        [StringLength(200, ErrorMessage = "Attach cannot be longer than 200 characters.")]
        public string Attach { get; set; }
        public int? AuthorID { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }
        public string Notes { get; set; }

        [ForeignKey("AuthorID")]
        public virtual SM_STAFFINFO Author { get; set; }

        [ForeignKey("Topic_ID")]
        public virtual TM_TOPIC Topic { get; set; }
    }
}