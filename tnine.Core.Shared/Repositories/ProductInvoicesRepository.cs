using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IProductInvoicesRepository : IRepository<ProductInvoices, long>
    {
    }

    public class ProductInvoicesRepository : Repository<ProductInvoices, long>, IProductInvoicesRepository
    {
        public ProductInvoicesRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
