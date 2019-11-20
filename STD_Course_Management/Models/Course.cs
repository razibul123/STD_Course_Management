using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STD_Course_Management.Models
{
    public class Course
    {

        public int Id { get; set; }

        [Required]
        public string Course_code { get; set; }

        [Required]
        public string Course_Name { get; set; }

        [Required]
        [Range(1.0, 4.0)]
        public double Credit { get; set; }

        [Required]
        public int DeptId { get; set; }
        public Dept Dept { get; set; }

        [Required]
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public List<Result> Results { get; set; }
    }
}
