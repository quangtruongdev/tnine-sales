using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IPaymentMethodsRepository : IRepository<PaymentMethods, long>
    {
    }

    public class PaymentMethodsRepository : Repository<PaymentMethods, long>, IPaymentMethodsRepository
    {
        public PaymentMethodsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
