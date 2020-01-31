using HireMe.Models;
using HireMe.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace HireMe.Areas.Identity.Pages.Account.Manage
{
    public partial class EditProfileModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;
        public EditProfileModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IWebHostEnvironment environment, 
            IConfiguration config)
        {
            _hostingEnvironment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
            this._config = config;
        }

        public string Username { get; set; }
        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

       [BindProperty]
        public IFormFile Picture { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Age")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
            public DateTime Age { get; set; }

            [Display(Name = "University Degrees ?")]
            public bool isUniversityDegrees { get; set; }

            public string PictureName { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                PictureName = user.PictureName
            };


            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            if (Input.Age != user.Age)
            {
                user.Age = Input.Age;
            }
            if (Input.PictureName != user.PictureName)
            {
                user.PictureName = Input.PictureName;
            }


            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            var dir = _config.GetSection("MySettings").GetSection("ProjectRootDir").Value;
            string folder = "uploads/UserProfile/Pictures/";
            string folderEdited = "uploads/UserProfile/Pictures/Edited/";

            var fileName = GetUniqueName(this.Picture.FileName);
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, folder);
            var filePath = Path.Combine(uploads, fileName);

            this.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

            var uploadPathWithfileName = Path.Combine(folder, fileName);
            var uploadAbsolutePath = Path.Combine(dir, uploadPathWithfileName);

            var uploadPathWithfileName_Edited = Path.Combine(folderEdited, fileName);
            var uploadAbsolutePath_Edited = Path.Combine(dir, uploadPathWithfileName_Edited);

            if (!IsImageFile(fileName))
            {
                 user.PictureName = uploadPathWithfileName;

            }
            else
            {
                StatusMessage = "Only pictures with .jpg, .png and .bmp are allowed.";
                return RedirectToPage();
            }


            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
           /* await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                */
            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
        private bool IsImageFile(string f)
        {
            return (f.EndsWith(".jpg"));
        }
    }
}
