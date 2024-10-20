using System.Collections.Generic;

namespace tnine.Core.Shared.Dto
{
    public class PagedResultDto<T> where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Results { get; set; }
    }
}
