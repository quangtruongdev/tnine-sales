using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IColorRepository : IRepository<Colors, long>
    {
    }

    public class ColorRepository : Repository<Colors, long>, IColorRepository
    {
        public ColorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
