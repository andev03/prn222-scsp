using BusinessLogic.IServices;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Forum
{
    public class IndexModel : PageModel
    {

        private readonly IForumService _forumService;

        public IndexModel(IForumService forumService)
        {
            _forumService = forumService;
        }
        public List<ForumPost> Posts { get; set; }

        public void OnGet()
        {
            Posts = _forumService.GetAll();
        }
    }
}
