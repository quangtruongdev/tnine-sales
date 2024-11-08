using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser, long>
    {
    }

    public class UserRepository : Repository<ApplicationUser, long>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
