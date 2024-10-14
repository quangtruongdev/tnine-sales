using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IProductRepository : IRepository<Product, long>
    {
    }

    public class ProductRepository : Repository<Product, long>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
