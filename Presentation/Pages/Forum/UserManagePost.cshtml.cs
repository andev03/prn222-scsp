using BusinessLogic.IServices;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Forum
{
    public class UserManagePostModel : PageModel
    {
        private readonly IForumService _forumService;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Content { get; set; }

        public UserManagePostModel(IForumService forumService)
        {
            _forumService = forumService;
        }

        public List<ForumPost> Posts { get; set; }
        public void OnGet()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            Posts = _forumService.GetAllByUserId(user.UserId);
        }

        public IActionResult OnPostEdit()
        {
            var post = _forumService.GetByPostId(Id);

            if (post == null)
            {
                TempData["ErrorMessage"] = "Bài viết không tồn tại.";
                return RedirectToPage();
            }

            post.Title = Title;
            post.Content = Content;

            _forumService.Update(post);

            TempData["SuccessMessage"] = "Cập nhật bài viết thành công!";
            return RedirectToPage();
        }

        public IActionResult OnPostDelete()
        {
            var post = _forumService.GetByPostId(Id);
            if (post == null)
            {
                TempData["ErrorMessage"] = "Bài viết không tồn tại.";
                return RedirectToPage();
            }
            post.IsDeleted = true;

            _forumService.Delete(post);

            TempData["SuccessMessage"] = "Xóa bài viết thành công!";
            return RedirectToPage();
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
