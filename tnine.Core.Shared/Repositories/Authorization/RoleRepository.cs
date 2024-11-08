using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IRoleRepository : IRepository<ApplicationRole, long>
    {
    }

    public class RoleRepository : Repository<ApplicationRole, long>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
