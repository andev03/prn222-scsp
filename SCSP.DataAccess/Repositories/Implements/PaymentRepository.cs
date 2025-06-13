using BusinessObjects.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SCSP.DataAccess.Repositories.Interfaces;

namespace SCSP.DataAccess.Repositories.Implements
{
    public class PaymentRepository : CrudRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DbContext context) : base(context)
        {
        }
    }
}
