using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IShopRepository : IRepository<Shop, long>
    {
    }

    public class ShopRepository : Repository<Shop, long>, IShopRepository
    {
        public ShopRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
