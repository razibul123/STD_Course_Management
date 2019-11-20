using STD_Course_Management.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STD_Course_Management.Models
{
    public class CourseConfirm
    {
        public int Id { get; set; }

        [Display (Name = "Student ID")]
        public string Student_id { get; set; }

        [ForeignKey("Student_id")]
        public virtual ApplicationUser Std { get; set; }

        public string Std_Id { get; set; }
        public bool is_confirm { get; set; }

    }
}
