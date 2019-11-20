using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STD_Course_Management.Models
{
    public class SelectedCourse
    {
        public int Id { get; set; }

        public int CourseConfirm_Id { get; set; }
        [ForeignKey("CourseConfirm_Id")]
        public virtual CourseConfirm CourseConfirm { get; set; }

        public int Course_Id { get; set; }
        [ForeignKey("Course_Id")]
        public virtual Course Course { get; set; }

    }
}
