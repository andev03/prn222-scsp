using BusinessLogic;
using BusinessLogic.IServices;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterInput Input { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Input.Password != Input.ConfirmPassword)
            {
                ModelState.AddModelError("Input.ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                return Page();
            }

            User user = new User();
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.Username = Input.Username;
            user.Email = Input.Email;
            user.Password = Input.Password;
            user.RoleId = "user";

            _userService.Register(user);

            return RedirectToPage("/Auth/Login");
        }

        public class RegisterInput
        {
            [Required]
            [Display(Name = "Tên")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Họ")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Tên đăng nhập")]
            public string Username { get; set; }

            [Required, EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required, DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            [Required, DataType(DataType.Password)]
            [Display(Name = "Xác nhận mật khẩu")]
            public string ConfirmPassword { get; set; }
        }
    }
}
