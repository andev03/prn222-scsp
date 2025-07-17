using BusinessLogic.IServices;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace Presentation.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ICommentService _commentService;
        private readonly IForumService _forumService;

        public IndexModel(ICommentService commentService, IForumService forumService)
        {
            _commentService = commentService;
            _forumService = forumService;
        }

        public ForumPost Post { get; set; }
        public List<ForumComment> Comments { get; set; }

        [BindProperty]
        public ForumComment NewComment { get; set; }

        public void OnGet(int postId)
        {
            Post = _forumService.GetByPostId(postId);
            Comments = _commentService.GetAllByPostId(postId);
        }

        public IActionResult OnPost(int postId)
        {
            if (!ModelState.IsValid)
            {
                Post = _forumService.GetByPostId(postId);
                Comments = _commentService.GetAllByPostId(postId);
                return Page();
            }

            User user = HttpContext.Session.GetObject<User>("user");
            NewComment.PostId = postId;
            NewComment.CreatedAt = DateTime.Now;
            NewComment.UserId = user.UserId;
            NewComment.PostId = postId;
            _commentService.AddComment(NewComment);

            return RedirectToPage(new { postId = postId });
        }
    }

}
