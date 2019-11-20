using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STD_Course_Management.Data;
using STD_Course_Management.Models;
using STD_Course_Management.Utilities;
using STD_Course_Management.ViewModels;

namespace STD_Course_Management.Controllers
{
    public class SelectedCourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelectedCourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            System.Security.Claims.ClaimsPrincipal CurrentUser = this.User;
            var ClaimsID = (ClaimsIdentity)this.User.Identity;
            var Claims = ClaimsID.FindFirst(ClaimTypes.NameIdentifier);

            CourseConfirmVM courseConfirmVM = new CourseConfirmVM()
            {
                CourseConfirms = new List<Models.CourseConfirm>()
            };
            courseConfirmVM.CourseConfirms = _context.CourseConfirm.Include(a => a.Std).ToList();
            if (User.IsInRole(SD.StudentUser))
            {
                courseConfirmVM.CourseConfirms = courseConfirmVM.CourseConfirms.Where(a => a.Student_id == Claims.Value).ToList();

            }
            return View(courseConfirmVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseList = (IEnumerable<Course>)(from p in _context.Course
                                                      join a in _context.SelectedCourse
                                                      on p.Id equals a.Course_Id
                                                      where a.CourseConfirm_Id == id
                                                      select p).Include("Dept");

            RegCrsVM objregcrsVM = new RegCrsVM()
            {
                CourseConfirm = _context.CourseConfirm.Include(a => a.Std).Where(a => a.Id == id).FirstOrDefault(),
                Std = _context.ApplicationUser.ToList(),
                Courses = courseList.ToList()
            };

            return View(courseList);

        } 
    }
}