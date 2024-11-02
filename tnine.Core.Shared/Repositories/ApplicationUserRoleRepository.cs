using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IApplicationUserRoleRepository : IRepository<ApplicationUserRole, long>
    {
    }

    public class ApplicationUserRoleRepository : Repository<ApplicationUserRole, long>, IApplicationUserRoleRepository
    {
        public ApplicationUserRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
