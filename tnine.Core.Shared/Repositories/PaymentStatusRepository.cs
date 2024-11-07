using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IPaymentStatusRepository : IRepository<PaymentStatus, long>
    {
    }

    public class PaymentStatusRepository : Repository<PaymentStatus, long>, IPaymentStatusRepository
    {
        public PaymentStatusRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
