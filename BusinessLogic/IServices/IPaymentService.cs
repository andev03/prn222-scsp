using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IPaymentService
    {
        Task<int> CreateSubscriptionAndPaymentAsync(Guid userId, string package, decimal amount, DateTime startDate, DateTime endDate, string paymentMethod);
    }
}
