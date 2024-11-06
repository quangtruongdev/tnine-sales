using System.Collections.Generic;

namespace tnine.Core.Shared.Dtos
{
    public class PagedResultDto<TEntity> where TEntity : class
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<TEntity> Results { get; set; }
        public PagedResultDto(int totalCount, IReadOnlyList<TEntity> results)
        {
            TotalCount = totalCount;
            Results = results;
        }
    }
}
