using BusinessObject.Models;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
    }
}