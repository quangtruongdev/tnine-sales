using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface ISupplierRepository : IRepository<Suppliers, long>
    {
    }

    public class SupplierRepository : Repository<Suppliers, long>, ISupplierRepository
    {
        public SupplierRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
