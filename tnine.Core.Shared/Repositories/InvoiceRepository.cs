using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice, long>
    {
    }

    public class InvoiceRepository : Repository<Invoice, long>, IInvoiceRepository
    {
        public InvoiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
