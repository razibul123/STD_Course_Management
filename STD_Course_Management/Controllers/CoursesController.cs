using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using STD_Course_Management.Data;
using STD_Course_Management.Models;
using STD_Course_Management.Utilities;

namespace STD_Course_Management.Controllers
{


    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Course.Include(c => c.Dept).Include(c => c.Semester).Include(c => c.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }


        [Authorize(Roles = SD.StudentUser + "," + SD.TeacherUser)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.Dept)
                .Include(c => c.Semester)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [Authorize(Roles = SD.AdminUser)]
        public IActionResult Create()
        {
            ViewData["DeptId"] = new SelectList(_context.Dept, "Id", "Name");
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Name");
            return View();
        }

        [Authorize(Roles = SD.AdminUser)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Course_code,Course_Name,Credit,DeptId,SemesterId,TeacherId")] Course course)
        {
            if (ModelState.IsValid)
            {
                var searchcourse = _context.Course.FirstOrDefault(c => c.Course_code == course.Course_code || c.Course_Name == course.Course_Name);

                if (searchcourse != null)
                {
                    ViewBag.msg = "This Course has been already inserted";
                    ViewData["DeptId"] = new SelectList(_context.Dept, "Id", "Name");
                    ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name");
                    ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Name");
                    return View(course);

                }

                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.Dept, "Id", "Name", course.DeptId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", course.SemesterId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Name", course.TeacherId);
            return View(course);
        }

        [Authorize(Roles = SD.StudentUser)]
        [HttpPost, ActionName("Index")]
        public async Task<IActionResult> RegCourse(int id)
        {
            List<int> coursefirst = HttpContext.Session.Get<List<int>>("crsRegisterd");
            if (coursefirst == null)
            {
                coursefirst = new List<int>();
            }
            coursefirst.Add(id);
            HttpContext.Session.Set("crsRegisterd", coursefirst);
            return RedirectToAction("Index");


        }

        [Authorize(Roles = SD.AdminUser)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["DeptId"] = new SelectList(_context.Dept, "Id", "Name", course.DeptId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", course.SemesterId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Name", course.TeacherId);
            return View(course);
        }


        [Authorize(Roles = SD.AdminUser)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Course_code,Course_Name,Credit,DeptId,SemesterId,TeacherId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.Dept, "Id", "Name", course.DeptId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", course.SemesterId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Name", course.TeacherId);
            return View(course);
        }


        [Authorize(Roles = SD.AdminUser)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.Dept)
                .Include(c => c.Semester)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


        [Authorize(Roles = SD.AdminUser)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}
