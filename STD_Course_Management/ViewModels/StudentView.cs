using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STD_Course_Management.ViewModels
{
    public class StudentView
    {
        public int Id { get; set; }

        [Required]
        public string Std_id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime Date_Birth { get; set; }

        public string Address { get; set; }

        public string Blood_group { get; set; }

        public IFormFile Std_image { get; set; }
    }
}
