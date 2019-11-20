using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STD_Course_Management.Models
{
    public class Semester
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Course> Courses { get; set; }



    }
}
