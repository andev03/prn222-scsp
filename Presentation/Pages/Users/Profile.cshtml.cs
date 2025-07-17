using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Models;
using BusinessLogic.IServices;
namespace Presentation.Pages.Users
{
    public class ProfileModel : PageModel
    {
        private readonly IUserService _userService;
        public ProfileModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string RoleName { get; set; }

        public void OnGet()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            RoleName = user.RoleId.ToUpper();
        }

        public IActionResult OnPost()
        {
            User user = HttpContext.Session.GetObject<User>("user");
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            if (!string.IsNullOrWhiteSpace(Password))
            {
                user.Password = Password;
            }

            _userService.UpdateProfile(user);

            HttpContext.Session.SetObject("user", user);

            TempData["Message"] = "Thông tin đã được cập nhật thành công!";
            return RedirectToPage();
        }
    }
}
