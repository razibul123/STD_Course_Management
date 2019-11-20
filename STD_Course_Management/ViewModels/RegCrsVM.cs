using STD_Course_Management.Data;
using STD_Course_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STD_Course_Management.ViewModels
{
    public class RegCrsVM
    {
        public CourseConfirm CourseConfirm { get; set; }
        public List<ApplicationUser> Std { get; set; }
        public List<Course> Courses { get; set; }

    }
}
