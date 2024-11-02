using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser, long>
    {
    }

    public class ApplicationUserRepository : Repository<ApplicationUser, long>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
