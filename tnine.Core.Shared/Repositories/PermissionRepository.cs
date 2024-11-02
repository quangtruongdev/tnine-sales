using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IPermissionRepository : IRepository<Permission, long>
    {
    }

    public class PermissionRepository : Repository<Permission, long>, IPermissionRepository
    {
        public PermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
