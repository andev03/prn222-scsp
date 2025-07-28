using BusinessObject.Models;
using DataAccess.IRepositories;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly QuitSmokingAppDBContext _context;
        public PaymentRepository(QuitSmokingAppDBContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }
    }
}