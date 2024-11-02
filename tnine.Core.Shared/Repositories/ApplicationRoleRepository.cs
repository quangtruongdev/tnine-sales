using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole, long>
    {
    }

    public class ApplicationRoleRepository : Repository<ApplicationRole, long>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
