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
    public class DeptsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public DeptsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = SD.StudentUser + "," + SD.TeacherUser + "," + SD.AdminUser)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dept.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dept = await _context.Dept
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dept == null)
            {
                return NotFound();
            }

            return View(dept);
        }

        [Authorize(Roles = SD.AdminUser)]
        // GET: Depts/Create
        public IActionResult Create()
        {
            return View();
        }



        [Authorize(Roles = SD.AdminUser)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Dept dept)
        {
            if (ModelState.IsValid)
            {
                var existsdept = _context.Dept.FirstOrDefault(d => d.Name == dept.Name);
                if (existsdept != null)
                {
                    ViewBag.msg = "This Deoartment has been already inserted";
                    return View(dept);
                }

                _context.Add(dept);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dept);
        }


        [Authorize(Roles = SD.AdminUser)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dept = await _context.Dept.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }



        [Authorize(Roles = SD.AdminUser)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Dept dept)
        {
            if (id != dept.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dept);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeptExists(dept.Id))
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
            return View(dept);
        }



        [Authorize(Roles = SD.AdminUser)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dept = await _context.Dept
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dept == null)
            {
                return NotFound();
            }

            return View(dept);
        }



        [Authorize(Roles = SD.AdminUser)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dept = await _context.Dept.FindAsync(id);
            _context.Dept.Remove(dept);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool DeptExists(int id)
        {
            return _context.Dept.Any(e => e.Id == id);
        }
    }
}
