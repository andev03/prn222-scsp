using BusinessLogic.IServices;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Leaderboard
{
    public class IndexModel : PageModel
    {
        private readonly IUserBadgeService _userBadgeService;

        public IndexModel(IUserBadgeService userBadgeService)
        {
            _userBadgeService = userBadgeService;
        }

        public List<NumberBadgeResponse> NumberBadgeResponses { get; set; }

        public void OnGet()
        {
            NumberBadgeResponses = _userBadgeService.NumberBadgesReceived();
        }
    }
}
