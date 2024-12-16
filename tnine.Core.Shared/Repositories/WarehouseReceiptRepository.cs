using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IWarehouseReceiptRepository : IRepository<WarehouseReceipt, long>
    {
    }

    public class WarehouseReceiptRepository : Repository<WarehouseReceipt, long>, IWarehouseReceiptRepository
    {
        public WarehouseReceiptRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
