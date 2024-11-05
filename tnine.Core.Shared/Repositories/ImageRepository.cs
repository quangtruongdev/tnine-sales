using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface IImageRepository : IRepository<Images, long>
    {
    }
    public class ImageRepository : Repository<Images, long>, IImageRepository
    {
        public ImageRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
