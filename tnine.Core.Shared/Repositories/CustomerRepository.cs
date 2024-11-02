using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, long>
    {
    }

    public class CustomerRepository : Repository<Customer, long>, ICustomerRepository
    {
        public CustomerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
