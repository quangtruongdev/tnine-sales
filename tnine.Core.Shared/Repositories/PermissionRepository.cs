using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IPermissionRepository : IRepository<Permission, long>
    {
        Task<List<ApplicationRole>> GetRolesByIdsAsync(List<long> roleIds);
        Task<List<Permission>> GetPermissionsByRoleIdsAsync(List<long> roleIds);
        Task<List<long>> GetRoleIdsByNamesAsync(string[] roleNames);
    }

    public class PermissionRepository : Repository<Permission, long>, IPermissionRepository
    {
        public PermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<ApplicationRole>> GetRolesByIdsAsync(List<long> roleIds)
        {
            using (var db = DbFactory.Init())
            {
                var query = from r in db.Roles
                            where roleIds.Contains(r.Id)
                            select r;

                return await query.ToListAsync();
            }
        }

        public async Task<List<Permission>> GetPermissionsByRoleIdsAsync(List<long> roleIds)
        {
            using (var db = DbFactory.Init())
            {
                var query = from p in db.Permissions
                            join rp in db.RolePermissions on p.Id equals rp.PermissionId
                            where roleIds.Contains(rp.RoleId)
                            select p;

                return await query.ToListAsync();
            }
        }

        public async Task<List<long>> GetRoleIdsByNamesAsync(string[] roleNames)
        {
            using (var db = DbFactory.Init())
            {
                var query = from r in db.Roles
                            where roleNames.Contains(r.Name)
                            select r.Id;

                return await query.ToListAsync();
            }
        }
    }
}
