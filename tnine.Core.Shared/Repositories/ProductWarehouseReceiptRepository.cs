using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IProductWarehouseReceiptRepository : IRepository<ProductWarehouseReceipt, long>
    {
    }

    public class ProductWarehouseReceiptRepository : Repository<ProductWarehouseReceipt, long>, IProductWarehouseReceiptRepository
    {
        public ProductWarehouseReceiptRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
