using BusinessLogic.IServices;
using BusinessObject.Models;
using DataAccess.IRepositories;
using System;
using System.Threading.Tasks;

namespace BusinessObject.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly ISubscriptionRepository _subscriptionRepo;

        public PaymentService(IPaymentRepository paymentRepo, ISubscriptionRepository subscriptionRepo)
        {
            _paymentRepo = paymentRepo;
            _subscriptionRepo = subscriptionRepo;
        }

        public async Task<int> CreateSubscriptionAndPaymentAsync(Guid userId, string package, decimal amount, DateTime startDate, DateTime endDate, string paymentMethod)
        {
            var subscription = new Subscription
            {
                UserId = userId,
                Package = package,
                StartDate = DateOnly.FromDateTime(startDate),
                EndDate = DateOnly.FromDateTime(endDate),
                Status = "active"
            };
            await _subscriptionRepo.AddSubscriptionAsync(subscription);

            var payment = new Payment
            {
                SubscriptionId = subscription.SubscriptionId,
                Amount = amount,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = paymentMethod,
                Status = "success"
            };
            await _paymentRepo.AddPaymentAsync(payment);

            return payment.PaymentId;
        }
    }
}