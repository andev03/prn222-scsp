using BusinessObject.Models;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface ISubscriptionRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
    }
}