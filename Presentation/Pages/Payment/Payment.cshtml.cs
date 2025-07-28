using BusinessLogic.IServices;
using BusinessObject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Presentation.Pages.Payment
{
    public class PaymentModel : PageModel
    {
        private readonly IPaymentService _paymentService;

        public PaymentModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [BindProperty(SupportsGet = true)]
        public int PackageId { get; set; }

        public string PackageName { get; set; }
        public decimal PackagePrice { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }


        public IActionResult OnGet()
        {
            ServiceFee = 0;
            Discount = 0;

            switch (PackageId)
            {
                case 1:
                    PackageName = "Gói Bạc";
                    PackagePrice = 99000;
                    break;
                case 2:
                    PackageName = "Gói Vàng";
                    PackagePrice = 199000;
                    Discount = 20000;
                    break;
                case 3:
                    PackageName = "Gói Kim Cương";
                    PackagePrice = 399000;
                    Discount = 50000;
                    break;
                default:
                    return RedirectToPage("/Index");
            }

            TotalAmount = PackagePrice + ServiceFee - Discount;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdString = HttpContext.Session.GetObject<string>("userId");
            if (userIdString == null)
                return RedirectToPage("/Auth/Login");

            if (!Guid.TryParse(userIdString, out Guid userId))
                return RedirectToPage("/Auth/Login");

            await _paymentService.CreateSubscriptionAndPaymentAsync(
                userId,
                PackageName,
                TotalAmount,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMonths(1),
                "QR"
            );

            return RedirectToPage("/PaymentSuccess");
        }
    }
}
