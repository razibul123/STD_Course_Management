using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STD_Course_Management.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string Teacher_Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Contact { get; set; }

        public string Blood_group { get; set; }

        public string Address { get; set; }

        public byte[] Photo { get; set; }

        public string About { get; set; }

        public List<Course> Courses { get; set; }


    }
}
