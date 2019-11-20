using STD_Course_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STD_Course_Management.ViewModels
{
    public class CourseRegistered
    {
        public List<Course> Courses { get; set; }
        public CourseConfirm CourseConfirm { get; set; }

    }
}
