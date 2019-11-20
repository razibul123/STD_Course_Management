using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using STD_Course_Management.Data;
using STD_Course_Management.Models;
using STD_Course_Management.ViewModels;
using STD_Course_Management.Utilities;

namespace STD_Course_Management.Controllers
{
    public class RegisterdCourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public CourseRegistered CourseRegisterdVM { get; set; }

        public RegisterdCourseController(ApplicationDbContext context)
        {
            _context = context;
            CourseRegisterdVM = new CourseRegistered()
            {
                Courses = new List<Models.Course>()
            };
        }

        public async Task<IActionResult> Index()
        {
            List<int> isRegistered = HttpContext.Session.Get<List<int>>("crsRegisterd");
            if (isRegistered.Count>0)
            { 
                foreach (int item in isRegistered)
                {
                    Course crs = _context.Course.Include(c => c.Dept).Include(c => c.Semester).Include(c=>c.Teacher).Where(c => c.Id == item).FirstOrDefault();
                    //Course crs = _context.Course.Where(c => c.Id == item).FirstOrDefault();
                    CourseRegisterdVM.Courses.Add(crs);
                }
            }

            return View(CourseRegisterdVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public async Task<IActionResult> Indexpost()
        {
            List<int> courselist = HttpContext.Session.Get<List<int>>("crsRegisterd");

           // CourseRegisterdVM.CourseConfirm = CourseRegisterdVM.CourseConfirm.Std_Id;
            CourseConfirm courseConfirm = CourseRegisterdVM.CourseConfirm;
            _context.CourseConfirm.Add(courseConfirm);
            await _context.SaveChangesAsync();

            int courseconfirmId = courseConfirm.Id;

            foreach (var crsID in courselist)
            {
                SelectedCourse selectedCourse = new SelectedCourse()
                {
                    CourseConfirm_Id = courseconfirmId,
                    Course_Id = crsID
                };
                _context.SelectedCourse.Add(selectedCourse);
            }
                await _context.SaveChangesAsync();
            courselist = new List<int>();
            HttpContext.Session.Set("crsRegisterd", courselist);
            return RedirectToAction("CourseConfirmation", "RegisterdCourse", new { Id = courseconfirmId });
            
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<int> courselist = HttpContext.Session.Get<List<int>>("crsRegisterd");
            if (courselist.Count>0)
            {
                if (courselist.Contains(id))
                {
                    courselist.Remove(id);
                }
            }
            HttpContext.Session.Set("crsRegisterd", courselist);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CourseConfirmation(int id)
        {
            CourseRegisterdVM.CourseConfirm = await _context.CourseConfirm.Where(c => c.Id == id).FirstOrDefaultAsync();
            List<SelectedCourse> objselectedCourses = await _context.SelectedCourse.Where(s => s.CourseConfirm_Id == id).ToListAsync();

            foreach (SelectedCourse item in objselectedCourses)
            {
                CourseRegisterdVM.Courses.Add(await _context.Course.Include(c => c.Dept).Include(c => c.Semester).Include(c => c.Teacher).Where(c => c.Id == item.Course_Id).FirstOrDefaultAsync());
            }

            return View(CourseRegisterdVM);

        }

    }
}
