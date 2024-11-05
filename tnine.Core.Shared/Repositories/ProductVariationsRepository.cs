using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IProductVariationsRepository : IRepository<ProductVariations, long>
    {
    }
    public class ProductVariationsRepository : Repository<ProductVariations, long>, IProductVariationsRepository
    {
        public ProductVariationsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
