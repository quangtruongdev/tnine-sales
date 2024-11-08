using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IUserRoleRepository : IRepository<ApplicationUserRole, long>
    {
    }

    public class UserRoleRepository : Repository<ApplicationUserRole, long>, IUserRoleRepository
    {
        public UserRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
