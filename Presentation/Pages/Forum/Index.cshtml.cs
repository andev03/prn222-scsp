using BusinessLogic.IServices;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Forum
{
    public class IndexModel : PageModel
    {

        private readonly IForumService _forumService;

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Content { get; set; }

        public IndexModel(IForumService forumService)
        {
            _forumService = forumService;
        }
        public List<ForumPost> Posts { get; set; }

        public void OnGet()
        {
            Posts = _forumService.GetAll();
        }

        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                Posts = _forumService.GetAll();
                return Page();
            }

            User user = HttpContext.Session.GetObject<User>("user");

            ForumPost forumPost = new ForumPost
            {
                Title = Title,
                Content = Content,
                UserId = user.UserId
            };

            _forumService.Create(forumPost);

            TempData["SuccessMessage"] = "Bài viết đã được đăng!";
            return RedirectToPage();
        }
    }
}