using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Dtos
{
    public class PagedResultDto<TEntity> where TEntity : class
    {
        public int TotalCount { get; set; }
        public IReadOnlyBasicRepository<TEntity> Results { get; set; }
    }
}
