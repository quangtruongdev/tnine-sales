using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IOrderRepository : IRepository<Orders, long>
    {
    }

    public class OrderRepository : Repository<Orders, long>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
