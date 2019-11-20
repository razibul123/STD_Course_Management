using Microsoft.AspNetCore.Identity;
using STD_Course_Management.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STD_Course_Management.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string User_id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        public string DeptName { get; set; }
       

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime Date_Birth { get; set; }

        public string Address { get; set; }

        public string Blood_group { get; set; }

        public string User_image { get; set; }

        [NotMapped]
        public string Role { get; set; }

    }
}
