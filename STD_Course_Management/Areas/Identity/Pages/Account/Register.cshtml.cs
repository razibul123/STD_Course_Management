using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using STD_Course_Management.Data;
using STD_Course_Management.Models;
using STD_Course_Management.Utilities;

namespace STD_Course_Management.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterModel(
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Student ID")]
            public string User_id { get; set; }


            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            public string Gender { get; set; }

            public string DeptName { get; set; }
          

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date of birth")]
            public DateTime Date_Birth { get; set; }

            public string Address { get; set; }


            [Display(Name = "Blood Group")]
            public string Blood_group { get; set; }

            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string User_image { get; set; }

            public string Role { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email,
                    User_id = Input.User_id,
                    Name = Input.Name,
                    Gender = Input.Gender,
                    Date_Birth = Input.Date_Birth,
                    DeptName = Input.DeptName,
                    Address = Input.Address,
                    Blood_group = Input.Blood_group,
                    PhoneNumber = Input.PhoneNumber,
                    User_image = Input.User_image,
                    Role = Input.Role
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(SD.StudentUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.StudentUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.TeacherUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.TeacherUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.AdminUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.AdminUser));
                    }
                    if (Input.Role == "Student")
                    {
                        await _userManager.AddToRoleAsync(user, SD.StudentUser);
                    }
                    if (Input.Role == "Teacher")
                    {
                        await _userManager.AddToRoleAsync(user, SD.TeacherUser);
                    }
                    if (Input.Role != "Student" && Input.Role != "Teacher")
                    {
                        await _userManager.AddToRoleAsync(user, SD.AdminUser);
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    ////await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                    return RedirectToAction("Index", "Admin");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
