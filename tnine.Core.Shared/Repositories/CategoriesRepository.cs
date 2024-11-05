using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface ICategoreisRepository : IRepository<Categories, long>
    {
    }

    public class CategoriesRepository : Repository<Categories, long>, ICategoreisRepository
    {
        public CategoriesRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
