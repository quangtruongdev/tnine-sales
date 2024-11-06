using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IRolePermissionRepository : IRepository<RolePermission, long>
    {
    }

    public class RolePermissionRepository : Repository<RolePermission, long>, IRolePermissionRepository
    {
        public RolePermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
