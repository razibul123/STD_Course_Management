using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace STD_Course_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<STD_Course_Management.Models.Course> Course { get; set; }
        public DbSet<STD_Course_Management.Models.Dept> Dept { get; set; }
        public DbSet<STD_Course_Management.Models.Result> Result { get; set; }
        public DbSet<STD_Course_Management.Models.Semester> Semester { get; set; }
        public DbSet<STD_Course_Management.Models.Student> Student { get; set; }
        public DbSet<STD_Course_Management.Models.Teacher> Teacher { get; set; }


        public DbSet<STD_Course_Management.Models.CourseConfirm> CourseConfirm { get; set; }
        public DbSet<STD_Course_Management.Models.SelectedCourse> SelectedCourse { get; set; }


      //  public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<STD_Course_Management.Data.ApplicationUser> ApplicationUser { get; set; }

    }
}
