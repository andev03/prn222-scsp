using BusinessObject.Models;
using DataAccess.IRepositories;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly QuitSmokingAppDBContext _context;
        public SubscriptionRepository(QuitSmokingAppDBContext context)
        {
            _context = context;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
        }
    }
}