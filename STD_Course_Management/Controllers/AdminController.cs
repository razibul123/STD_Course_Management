using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STD_Course_Management.Data;
using STD_Course_Management.Utilities;

namespace STD_Course_Management.Controllers
{
    // [Authorize(Roles =SD.AdminUser)]

    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_context.ApplicationUser.ToList());
        }


        public IActionResult Search(string searchtextName, string searchtextDept, string searchtextPhone, string searchtextId)
        {
            IQueryable<ApplicationUser> appuser = _context.ApplicationUser;
            ViewBag.StextNAME = searchtextName;
            ViewBag.StextDEPT = searchtextDept;
            ViewBag.StextPHONE = searchtextPhone;
            ViewBag.StextID = searchtextId;


            if (!string.IsNullOrEmpty(searchtextName) || !string.IsNullOrEmpty(searchtextDept) || !string.IsNullOrEmpty(searchtextPhone) || !string.IsNullOrEmpty(searchtextId))
            {
                //if (searchtextName.Trim().Length>0)
                //{
                //    searchtextName = searchtextName.ToLower();
                //}
                //if (searchtextDept.Trim().Length > 0)
                //{
                //    searchtextDept = searchtextDept.ToLower();
                //}
                //if (searchtextPhone.Trim().Length > 0)
                //{
                //    searchtextPhone = searchtextPhone.ToLower();
                //}
                //if (searchtextId.Trim().Length > 0)
                //{
                //    searchtextId = searchtextId.ToLower();
                //}

                appuser = appuser.Where(a => a.UserName.ToLower().Contains(searchtextName)
                || a.DeptName.ToLower().Contains(searchtextDept)
                || a.PhoneNumber.Contains(searchtextPhone)
                || a.User_id.ToLower().Contains(searchtextId));
            }

            return View(appuser.ToList());
        }

        //Get Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var userFromDb = await _context.ApplicationUser.FindAsync(id);
            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }


        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ApplicationUser userFromDb = _context.ApplicationUser.Where(u => u.Id == id).FirstOrDefault();

                userFromDb.User_id = applicationUser.User_id;
                userFromDb.Name = applicationUser.Name;
                userFromDb.Gender = applicationUser.Gender;
                userFromDb.Date_Birth = applicationUser.Date_Birth;
                userFromDb.Address = applicationUser.Address;
                userFromDb.Blood_group = applicationUser.Blood_group;
                userFromDb.User_image = applicationUser.User_image;


                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(applicationUser);
        }


        //Get Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var userFromDb = await _context.ApplicationUser.FindAsync(id);
            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }


        //Post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string id)
        {
            ApplicationUser userFromDb = _context.ApplicationUser.Where(u => u.Id == id).FirstOrDefault();
            userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}