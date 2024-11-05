using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface ISizeRepository : IRepository<Sizes, long>
    {
    }
    public class SizeRepository : Repository<Sizes, long>, ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
