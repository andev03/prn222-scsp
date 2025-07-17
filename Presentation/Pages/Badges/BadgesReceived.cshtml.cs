using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Models;
using BusinessLogic;
using BusinessLogic.IServices;

namespace Presentation.Pages.Badges
{
    public class BadgesReceivedModel : PageModel
    {
        private readonly IBadgeService _badgeService;

        public BadgesReceivedModel(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        public List<Badge> Badges { get; set; }

        public void OnGet()
        {
            User user = HttpContext.Session.GetObject<User>("user");
            Badges = _badgeService.ListBadgesUserReceived(user);
        }
    }
}
